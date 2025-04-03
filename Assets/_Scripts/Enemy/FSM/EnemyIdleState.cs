using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    private float idleTimer;
    private const float idleDuration = 5f; // wait 5 seconds

    public override void EnterState(EnemyMovementBehaviour state)
    {
        idleTimer = 0f;
        state.StopMovement();
        Debug.Log($"{state.gameObject.name} entered Idle state");
    }

    public override void ExecuteState(EnemyMovementBehaviour enemy)
    {
        if(enemy.DoesPatrol)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                enemy.ChangeState(new EnemyPatrolState());
            }
        }
    }

    public override void ExitState(EnemyMovementBehaviour state)
    {

    }
}
