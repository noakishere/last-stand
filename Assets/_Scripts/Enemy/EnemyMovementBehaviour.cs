using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    [SerializeField] private Transform target;
    public Transform Target => target;
    private float attackDistance;
    public float AttackDistance => attackDistance;

    private float letGoDistance;
    public float LetGoDistance => letGoDistance;

    private float pathUpdateDeadline;

    [SerializeField] private EnemyStateBase currentState;
    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        attackDistance = enemyReferences.NavMeshAgent.stoppingDistance;
        letGoDistance = Settings.EnemyLetGoDistance;

        ChangeState(new EnemyIdleState());
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

    public void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    public void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            Debug.Log($"{gameObject.name}: Updating Path");
            pathUpdateDeadline = Time.time + enemyReferences.PathUpdateDelay;
            enemyReferences.NavMeshAgent.SetDestination(target.position);
        }
    }

    public void StopMovement()
    {
        enemyReferences.NavMeshAgent.SetDestination(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ChangeState(new EnemyFollowState());
        }
    }
}
