using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Movement;
using Island.Combat;
using Island.Core;
using UnityEngine.AI;
using System;

namespace Island.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float _chaseDistance = 5f;
        private Mover _mover;
        private Fighter _fighter;
        private GameObject _player;
        private Health _health;
        private Vector3 _guardOriginPosition;
        private ActionScheduler _actionScheduler;
        private float _lastTimeSeenPlayer = 0;
        private float _waitTimeAndSearchPlayer = 5f;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            if (_player == null)
                Debug.LogError("Player is NULL.");

            _mover = GetComponent<Mover>();
            if (_mover == null)
            {
                Debug.LogError("Mover is NULL.");
            }

            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
            {
                Debug.LogError("Fighter is NULL.");
            }

            _health = GetComponent<Health>();
            if (_health == null)
            {
                Debug.Log("Health is NULL.");
            }

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
            {
                Debug.LogError("ActionScheduler is NULL.");
            }

            _guardOriginPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead()) 
            {
                return;
            }
            if (InAttackRangeCalculation() && _fighter.CanAttack(_player))
            {
                AttackBehaviour();           
            }
            else if (Time.time < _waitTimeAndSearchPlayer + _lastTimeSeenPlayer)
            {
                WaitBehaviour();
            }
            else
            {
                ReturnBehaviour();
            }
        }

        private void ReturnBehaviour()
        {
            _mover.StartMoveAction(_guardOriginPosition);
        }

        private void AttackBehaviour()
        {
            _fighter.Attack(_player);
            _lastTimeSeenPlayer = Time.time;
        }

        private void WaitBehaviour()
        {
            _actionScheduler.CancelAction();
        }

        private bool InAttackRangeCalculation()
        {
            float distance = Mathf.Abs(Vector3.Distance(_player.transform.position, transform.position));
            return (distance < _chaseDistance);     
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.5F);
            Gizmos.DrawSphere(transform.position, _chaseDistance);
        }
    }
}
