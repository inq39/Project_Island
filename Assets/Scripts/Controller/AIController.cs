using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Movement;
using Island.Combat;
using Island.Core;

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
        }

        private void Update()
        {
            if (_health.IsDead()) { return; }
            if (InAttackRangeCalculation() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
        }

        private bool InAttackRangeCalculation()
        {
            float distance = Mathf.Abs(Vector3.Distance(_player.transform.position, transform.position));
            return (distance < _chaseDistance);     
        }
    }


}
