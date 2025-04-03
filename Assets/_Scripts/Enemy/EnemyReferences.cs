using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent => navMeshAgent;

    private CapsuleCollider enemyCollider;
    public CapsuleCollider Collider => enemyCollider;

    [Header("Stats")]
    private float pathUpdateDelay = 0.2f;
    public float PathUpdateDelay => pathUpdateDelay;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<CapsuleCollider>();

        navMeshAgent.stoppingDistance = Settings.EnemyStoppingDistance;
    }

}
