using UnityEngine;
using Island.Movement;
using Island.Core;

namespace Island.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        private float _weaponRange = 2f;
        [SerializeField]
        private float _timeBetweenAttacks = 2f;
        private Health _target;
        private Mover _mover;
        private bool _isInRange;
        private Animator _playerAnimator;
        private float _lastTimeAttack;
        [SerializeField]
        private float _damageValue;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            
            _playerAnimator = GetComponent<Animator>();
            if (_playerAnimator == null)
            {
                Debug.LogError("Animator is NULL.");
            }

        }
        private void Update()
        {
            if (_target == null) return;
            if (_target.IsDead()) return;

            if (!GetIsInRange())
            {               
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Cancel();
                AttackingBehavior();
            }
        }

        private void AttackingBehavior()
        {
            if (Time.time >= _lastTimeAttack + _timeBetweenAttacks)
            {
                _playerAnimator.SetTrigger("attack");
                
                _lastTimeAttack = Time.time;
            }        
        }

        //Animation Event
        void Hit() 
        {        
            _target.TakeDamage(_damageValue);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
            if (_target == null)
            {
                Debug.LogError("Health of Target is NULL.");
            }
        }

        public void Cancel()
        {
            _target = null;
            _playerAnimator.SetTrigger("stopAttack");
        }

        
     
    }
}