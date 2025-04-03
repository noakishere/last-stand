using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBehaviour : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    public EnemyReferences EnemyReferences => enemyReferences;

    [SerializeField] private Transform target;
    public Transform Target => target;
    private float attackDistance;
    public float AttackDistance => attackDistance;

    private float letGoDistance;
    public float LetGoDistance => letGoDistance;

    [SerializeField] private EnemyStateBase currentState;

    [Header("Patrol Settings")]
    [SerializeField] private bool doesPatrol;
    public bool DoesPatrol => doesPatrol;
    public float PatrolRange = 10f;
    public Vector3 PatrolCenter { get; private set; }

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();

        PatrolCenter = transform.position;
    }

    void Start()
    {
        attackDistance = enemyReferences.NavMeshAgent.stoppingDistance;
        letGoDistance = Settings.EnemyLetGoDistance;

        //currentState = Target == null ? new EnemyPatrolState() : new EnemyFollowState();

        //ChangeState(currentState);
        ChangeState(EnemyIdleState.Instance);
    }

    void Update()
    {
        currentState?.ExecuteState(this);
    }

    public void ChangeState(EnemyStateBase newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        currentState.EnterState(this);
    }   

    public void StopMovement()
    {
        enemyReferences.NavMeshAgent.SetDestination(transform.position);
    }

    public void SetTarget(NavMeshHit hit)
    {
        enemyReferences.NavMeshAgent.SetDestination(hit.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ChangeState(EnemyFollowState.Instance);
        }
    }
}
