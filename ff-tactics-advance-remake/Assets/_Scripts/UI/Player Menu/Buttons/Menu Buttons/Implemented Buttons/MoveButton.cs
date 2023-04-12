public class MoveButton : MenuButton
{
    protected override void ExecuteAction()
    {
        EventManager.OnCommandSent?.Invoke(new MovementCommand());
        
        GameManager.Instance.movementState.activateMovement = true;
        GameManager.Instance.ChangeState(GameManager.Instance.movementState);
    }
}