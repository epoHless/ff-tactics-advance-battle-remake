using System;

public class StatusButton : MenuButton
{
    protected override void ExecuteAction()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.statusState);
    }
}
