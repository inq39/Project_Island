using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100;
        private Animator _animator;
        private bool _isDead = false;
        private ActionScheduler _actionScheduler;
        public bool IsDead()
        {
            return _isDead;
        }
        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null) 
                Debug.LogError("Animator is NULL.");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
                Debug.LogError("ActionScheduler is NULL.");
        }

        public void TakeDamage(float damage)
        {           
            _health = Mathf.Max(_health - damage, 0);

            if (_health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) { return; }
            _isDead = true;
            _animator.SetTrigger("die");
            _actionScheduler.CancelAction();          
        }
    }
}
