using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Island.Core;

namespace Island.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _playerNavMeshAgent;
        private Animator _playerAnimator;
        private Health _health;

        // Start is called before the first frame update
        void Start()
        {
            _playerNavMeshAgent = GetComponent<NavMeshAgent>();
            if (_playerNavMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent is NULL.");
            }

            _health = GetComponent<Health>();
            if (_health == null)
            {
                Debug.Log("Health is NULL.");
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
            _playerNavMeshAgent.enabled = !_health.IsDead();
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
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _playerNavMeshAgent.SetDestination(destination);
            
            _playerNavMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _playerNavMeshAgent.isStopped = true;
            
        }

       
    }
}
