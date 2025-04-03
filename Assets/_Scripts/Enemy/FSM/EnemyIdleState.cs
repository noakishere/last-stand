using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    public override void EnterState(EnemyMovementBehaviour state)
    {
        state.StopMovement();
    }

    public override void ExecuteState(EnemyMovementBehaviour state)
    {

    }

    public override void ExitState(EnemyMovementBehaviour state)
    {

    }
}
