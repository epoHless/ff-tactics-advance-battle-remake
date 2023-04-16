using System;
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
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void LoadScreen()
    {
        LeanTween.value(gameObject, f => { canvasGroup.alpha = f; }, 0, 1, .5f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, f => { loadingBar.fillAmount = f; }, 0, 1, 5f).setOnComplete((() =>
            {
                SceneManager.LoadSceneAsync(1).completed += operation =>
                {
                    LeanTween.value(gameObject, f => { canvasGroup.alpha = f; }, 1, 0, .5f).setOnComplete(() => gameObject.SetActive(false));
                };
            }));
        });
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
