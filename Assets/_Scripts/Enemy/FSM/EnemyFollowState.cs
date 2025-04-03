using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFollowState : EnemyStateBase
{
    private float pathUpdateDeadline;
    public override void EnterState(EnemyMovementBehaviour state)
    {
        
    }

    public override void ExecuteState(EnemyMovementBehaviour enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Target.position);
        if (distance <= enemy.AttackDistance)
        {
            //state.ChangeState(new EnemyAttackState());
            Debug.Log($"{enemy.gameObject.name}: Attack!!");
        }
        else if (distance > enemy.LetGoDistance)
        {
            enemy.ChangeState(new EnemyIdleState());
        }
        else
        {
            UpdatePath(enemy);
            LookAtTarget(enemy);
        }
    }

    public override void ExitState(EnemyMovementBehaviour state)
    {
        
    }

    public void LookAtTarget(EnemyMovementBehaviour enemy)
    {
        Vector3 lookPos = enemy.Target.position - enemy.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, 0.2f);
    }

    public void UpdatePath(EnemyMovementBehaviour enemy)
    {
        if (Time.time >= pathUpdateDeadline)
        {
            Debug.Log($"{enemy.gameObject.name}: Updating Path");
            pathUpdateDeadline = Time.time + enemy.EnemyReferences.PathUpdateDelay;
            enemy.EnemyReferences.NavMeshAgent.SetDestination(enemy.Target.position);
        }
    }
}
