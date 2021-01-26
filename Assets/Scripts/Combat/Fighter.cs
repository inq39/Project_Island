﻿using UnityEngine;
using Island.Movement;
using Island.Core;

namespace Island.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        private float _weaponRange = 2f;
        private Transform _target;
        private Mover _mover;
        private bool _isInRange;

        private void Start()
        {
            _mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (_target == null) return;
            

            if (!GetIsInRange())
            {               
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.transform;           
        }

        public void Cancel()
        {
            _target = null;
        }

       
    }
}