public abstract class EnemyStateBase
{
    public abstract void EnterState(EnemyMovementBehaviour state);
    public abstract void ExecuteState(EnemyMovementBehaviour state); //used for update functions
    public abstract void ExitState(EnemyMovementBehaviour state);
}
