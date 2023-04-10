public class MoveButton : MenuButton
{
    protected override void ExecuteAction()
    {
        GameManager.Instance.movementState.activateMovement = true;
        GameManager.Instance.ChangeState(GameManager.Instance.movementState);
    }
}
