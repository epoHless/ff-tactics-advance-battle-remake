public class PauseButton : MenuButton
{
    protected override void ExecuteAction()
    {
        LoadingScreen.Instance.LoadScreen(0);
    }
}
