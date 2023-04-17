using System;
using FinalFantasy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : Singleton<LoadingScreen>
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        InputSystem.EnableNavigation();
    }

    public void LoadScreen(int index)
    {
        loadingBar.fillAmount = 0;
        
        LeanTween.value(gameObject, f => { canvasGroup.alpha = f; }, 0, 1, .5f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, f => { loadingBar.fillAmount = f; }, 0, 1, 5f).setOnComplete((() =>
            {
                SceneManager.LoadSceneAsync(index).completed += operation =>
                {
                    LeanTween.value(gameObject, f => { canvasGroup.alpha = f; }, 1, 0, .5f).setOnComplete(() =>
                    {
                        canvasGroup.alpha = 0;
                        canvasGroup.interactable = false;
                        canvasGroup.blocksRaycasts = false;
                    });
                };
            }));
        });
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
