public class WaitButton : MenuButton
{
    protected override void ExecuteAction()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.facingDirectionState);
        //todo add character turn speedup turn
    }
}
