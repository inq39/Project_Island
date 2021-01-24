using UnityEngine;
using Island.Movement;

namespace Island.Combat 
{
    public class Fighter : MonoBehaviour
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
                _mover.StopMoving();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;           
        }

        public void CancelAttack()
        {
            _target = null;
        }
    }
}