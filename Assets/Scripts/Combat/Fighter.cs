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
        private float _lastTimeAttack = Mathf.NegativeInfinity;
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
                _mover.MoveTo(_target.transform.position, 1f);
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
                transform.LookAt(_target.transform);
                TriggerAttack();

                _lastTimeAttack = Time.time;
            }
        }

        private void TriggerAttack()
        {
            _playerAnimator.ResetTrigger("stopAttack");
            _playerAnimator.SetTrigger("attack");
        }

        //Animation Event
        void Hit() 
        {
            if (_target == null) { return; };
            _target.TakeDamage(_damageValue);
        }

        private bool GetIsInRange()
        {
            return Mathf.Abs(Vector3.Distance(transform.position, _target.transform.position)) < _weaponRange;
        }

        public void Attack(GameObject combatTarget)
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
            
            TriggerCancel();
            _target = null;
            _mover.Cancel();
        }

        private void TriggerCancel()
        {
            _playerAnimator.ResetTrigger("attack");
            _playerAnimator.SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return (targetToTest != null && !targetToTest.IsDead());         
        }
     
    }
}