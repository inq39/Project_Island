using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent _playerNavMeshAgent;
    private Animator _playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _playerNavMeshAgent = GetComponent<NavMeshAgent>();
        if (_playerNavMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is NULL.");
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
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 localPlayerVelocity = transform.InverseTransformDirection(_playerNavMeshAgent.velocity);
        float playerSpeed = localPlayerVelocity.z;
        _playerAnimator.SetFloat("moveForward", playerSpeed);
    }

    private void MoveToCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity);
        
        

        if (hasHit && hit.collider.name == "Terrain")
        {
            _playerNavMeshAgent.SetDestination(hit.point);          
        }
        
    }
}
