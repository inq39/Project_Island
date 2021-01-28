using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100;
        private Animator _animator;
        private bool _isDead = false;
        public bool IsDead()
        {
            return _isDead;
        }
        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null) 
                Debug.LogError("Animator is NULL.");
        }

        public void TakeDamage(float damage)
        {
            /*if (damage >= _health)
            {
                _health = 0;
            }
            _health -= damage;
            better: */
            _health = Mathf.Max(_health - damage, 0);

            if (_health == 0 && !_isDead)
            {
                _animator.SetTrigger("die");
                _isDead = true;
            }
        }
    }
}
