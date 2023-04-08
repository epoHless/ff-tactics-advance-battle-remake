public class MoveButton : MenuButton
{
    protected override void ExecuteAction()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.movementState);
    }
}
