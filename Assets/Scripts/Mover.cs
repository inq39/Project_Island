using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private NavMeshAgent _playerNavMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        _playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerNavMeshAgent.SetDestination(_target.position);
    }
}
