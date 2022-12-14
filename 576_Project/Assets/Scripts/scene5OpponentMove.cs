using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class scene5OpponentMove : MonoBehaviour
{
   [SerializeField] NavMeshAgent navmeshAgent;
    [SerializeField] Transform targetPosition;
    [SerializeField]
    private Animator animation_controller;

    [SerializeField] private float destinationReachedTreshold;



    void Start(){
        animation_controller = GetComponent<Animator>();
        destinationReachedTreshold = 32.0f;
    }

    bool CheckDestinationReached() {
    Vector3  dist = targetPosition.position - transform.position ;
    // Debug.Log(dist);
    float distanceToTarget = dist.sqrMagnitude;
    Debug.Log(distanceToTarget);
    if(distanceToTarget < destinationReachedTreshold)
        {
            animation_controller.SetBool("isWalkingForward",false);
            animation_controller.Play("Idle");
            return true;
        }
    else{
        return false;
    }
    }

    void Update () {
        navmeshAgent.SetDestination(targetPosition.position);
        animation_controller.SetBool("isWalkingForward", navmeshAgent.velocity.magnitude > 0.01f);

        Vector3  dist = targetPosition.position - transform.position ;
        Vector3 direction = dist;
        direction.y = 0.0f;
        direction.Normalize();
        float distanceToTarget = dist.sqrMagnitude;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
        if(distanceToTarget < destinationReachedTreshold)
            {
                animation_controller.SetBool("isWalkingForward",false);
                animation_controller.Play("Idle");
            }



    }
}
