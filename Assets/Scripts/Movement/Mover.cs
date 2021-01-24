using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Island.Combat;

namespace Island.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent _playerNavMeshAgent;
        private Animator _playerAnimator;
        private Fighter _fighter;

        // Start is called before the first frame update
        void Start()
        {
            _playerNavMeshAgent = GetComponent<NavMeshAgent>();
            if (_playerNavMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent is NULL.");
            }

            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
            {
                Debug.LogError("Fighter is NULL.");
            }

            _playerAnimator = GetComponent<Animator>();
            if (_playerAnimator == null)
            {
                Debug.LogError("Animator is NULL.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 localPlayerVelocity = transform.InverseTransformDirection(_playerNavMeshAgent.velocity);
            float playerSpeed = localPlayerVelocity.z;
            _playerAnimator.SetFloat("moveForward", playerSpeed);
        }

        public void StartMoveAction(Vector3 destination)
        {
            _fighter.CancelAttack();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _playerNavMeshAgent.SetDestination(destination);
            _playerNavMeshAgent.isStopped = false;
        }

        public void StopMoving()
        {
            _playerNavMeshAgent.isStopped = true;
            
        }
    }
}
