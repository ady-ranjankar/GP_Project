using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveOpponentNavMesh : MonoBehaviour
{

    private GameObject opp;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        opp = GameObject.Find("opp");
        Debug.Log("Opponent Find");
        navMeshAgent = opp.GetComponent<NavMeshAgent>();
        destination = GameObject.Find("Destination");
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(destination.transform.position);
    }
}
