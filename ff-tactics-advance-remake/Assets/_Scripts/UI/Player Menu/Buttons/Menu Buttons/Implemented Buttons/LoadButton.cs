using UnityEngine;

public class LoadButton : MonoBehaviour
{
    public void LoadGame(int i)
    {
        LoadingScreen.Instance.LoadScreen(i);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
