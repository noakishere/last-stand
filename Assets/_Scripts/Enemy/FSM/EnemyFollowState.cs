using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyStateBase
{
    public override void EnterState(EnemyMovementBehaviour state)
    {
        
    }

    public override void ExecuteState(EnemyMovementBehaviour state)
    {
        float distance = Vector3.Distance(state.transform.position, state.Target.position);
        if (distance <= state.AttackDistance)
        {
            //state.ChangeState(new EnemyAttackState());
            Debug.Log($"{state.gameObject.name}: Attack!!");
        }
        else if (distance > state.LetGoDistance)
        {
            state.ChangeState(new EnemyIdleState());
        }
        else
        {
            state.UpdatePath();
            state.LookAtTarget();
        }
    }

    public override void ExitState(EnemyMovementBehaviour state)
    {
        
    }
}
