using GridSystem;

public class MoveButton : MenuButton
{
    #region Methods

    public override bool CanBeEnabled()
    {
        return !TurnManager.Instance.currentTurn.HasMoved;
    }

    protected override void ExecuteAction()
    {
        EventManager.OnCommandSent?.Invoke(new MovementCommand());
        
        GameManager.Instance.movementState.activateMovement = true;
        GameManager.Instance.ChangeState(GameManager.Instance.movementState);
    }

    #endregion
}