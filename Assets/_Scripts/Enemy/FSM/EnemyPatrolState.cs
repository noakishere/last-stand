using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyStateBase
{
    public static EnemyPatrolState Instance { get; } = new EnemyPatrolState();
    private Vector3 patrolDestination;

    public EnemyPatrolState() { }

    public override void EnterState(EnemyMovementBehaviour enemy)
    {
        Debug.Log($"{enemy.gameObject.name} entered Patrol state");
        SetNewDestination(enemy);
    }

    public override void ExecuteState(EnemyMovementBehaviour enemy)
    {
        if (!enemy.EnemyReferences.NavMeshAgent.pathPending &&
            enemy.EnemyReferences.NavMeshAgent.remainingDistance <= enemy.EnemyReferences.NavMeshAgent.stoppingDistance)
        {
            enemy.ChangeState(EnemyIdleState.Instance);
        }
    }

    public override void ExitState(EnemyMovementBehaviour state)
    {
    }

    private void SetNewDestination(EnemyMovementBehaviour enemy)
    {
        Vector3 randomDirection = Random.insideUnitSphere * enemy.PatrolRange;
        randomDirection.y = 0; // keep the destination on a horizontal plane
        patrolDestination = enemy.PatrolCenter + randomDirection;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(patrolDestination, out navHit, enemy.PatrolRange, NavMesh.AllAreas))
        {
            enemy.SetTarget(navHit);
            //Debug.Log($"{enemy.name} patrol destination set to {navHit.position}");
        }
    }
}
