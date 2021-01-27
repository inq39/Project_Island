using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Island.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100;

        public void TakeDamage(float damage)
        {
            /*if (damage >= _health)
            {
                _health = 0;
            }
            _health -= damage;
            better: */
            _health = Mathf.Max(_health - damage, 0);
        }
    }
}
