using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scene5PlayerMove : MonoBehaviour
{
   [SerializeField] NavMeshAgent navmeshAgent;
    [SerializeField] Transform targetPosition;
    void Update () {
        navmeshAgent.SetDestination(targetPosition.position);
    }
}
