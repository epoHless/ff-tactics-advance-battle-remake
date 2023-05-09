public class PauseButton : MenuButton
{
    protected override void ExecuteAction()
    {
        LoadingScreen.Instance.LoadScreen(0);
    }

    public void LoadGame()
    {
        LoadingScreen.Instance.LoadScreen(1);
    }
}
