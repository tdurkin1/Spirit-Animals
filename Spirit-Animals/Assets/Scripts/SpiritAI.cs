using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiritAI : MonoBehaviour
{
    [Tooltip("Navmesh Agent Target. Set to Spirit Target")]
    [SerializeField] Transform spiritTarget;

    [Tooltip("Turning Speed. Default is 4")]
    [SerializeField] float turnSpeed = 4f;

    private Camera mainCamera;

    NavMeshAgent navMeshAgent;
    Animator animController;
    float distToTarget = Mathf.Infinity;

    void Start()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
    }

    void Update()
    {
        distToTarget = Vector3.Distance(transform.position, spiritTarget.position);
        if (distToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
            animController.SetBool("isWalking", true);
        } else
        {
            FaceCamera();
            animController.SetBool("isWalking", false);
        }
    }

    private void FaceCamera()
    {
        
        Vector3 lookDirection = (mainCamera.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(spiritTarget.position);
    }
}
