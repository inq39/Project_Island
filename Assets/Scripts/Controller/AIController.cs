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
        [SerializeField]
        private float _waitTimeAndSearchPlayer = 5f;
        [SerializeField]
        private PatrolPath _patrolPath;
        private float _lastTimeSeenPlayer = 0;
        [SerializeField]
        private float _patrolPathTolerance = 1f;
        private Mover _mover;
        private Fighter _fighter;
        private GameObject _player;
        private Health _health;
        private Vector3 _guardOriginPosition;
        private ActionScheduler _actionScheduler;
        private int _patrolPathIndex = 0;
        private float _lastWaitAtPatrolPoint = 0f;
        private float _waitTime = 3f;

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
                PatrolBehaviour();
            }
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardOriginPosition;

            if (_patrolPath != null)
            {
                if (AtWayPoint())
                {
                    if (_lastWaitAtPatrolPoint + _waitTime < Time.time)
                    {
                        CycleWayPoint();
                    }                   
                }
                nextPosition = GetCurrentWaypoint();
            }
            _mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.transform.GetChild(_patrolPathIndex).transform.position;
        }

        private void CycleWayPoint()
        {
            if (_patrolPathIndex < _patrolPath.transform.childCount -1)
            {
                _patrolPathIndex++;
                _lastWaitAtPatrolPoint = Time.time;
            }
            else
            {
                _patrolPathIndex = 0;
                _lastWaitAtPatrolPoint = Time.time;
            }
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWaypoint());     
            return (distanceToWayPoint <= _patrolPathTolerance);
           
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
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            return (distance < _chaseDistance);     
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.5F);
            Gizmos.DrawSphere(transform.position, _chaseDistance);
        }
    }
}
