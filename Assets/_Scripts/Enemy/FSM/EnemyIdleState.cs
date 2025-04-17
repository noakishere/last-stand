using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    public static EnemyIdleState Instance { get; } = new EnemyIdleState();
    private float idleTimer;
    private const float idleDuration = 5f; // wait 5 seconds

    public EnemyIdleState() { }

    public override void EnterState(EnemyMovementBehaviour state)
    {
        idleTimer = 0f;
        state.StopMovement();
        //Debug.Log($"{state.gameObject.name} entered Idle state");
    }

    public override void ExecuteState(EnemyMovementBehaviour enemy)
    {
        if(enemy.DoesPatrol)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                enemy.ChangeState(EnemyPatrolState.Instance);
            }
        }
    }

    public override void ExitState(EnemyMovementBehaviour state)
    {

    }
}
