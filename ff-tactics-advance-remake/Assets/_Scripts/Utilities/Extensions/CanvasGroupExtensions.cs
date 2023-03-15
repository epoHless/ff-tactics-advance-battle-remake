using System;
using UnityEngine;

public static class CanvasGroupExtensions
{
    /// <summary>
    /// Toggle the item on or off in a set amount of time
    /// </summary>
    /// <param name="_canvasGroup"></param>
    /// <param name="_toggle"></param>
    /// <param name="time"></param>
    /// <param name="OnComplete"></param>
    public static void Toggle(this CanvasGroup _canvasGroup, bool _toggle, float time, Action OnComplete = null)
    {
        float from = _toggle ? 0 : 1;
        float to = _toggle ? 1 : 0;
    
        LeanTween.value(_canvasGroup.gameObject, f =>
        {
            _canvasGroup.alpha = f;
        }, from, to, time).setOnComplete(OnComplete);
    
        ToggleInteraction(_canvasGroup, _toggle);
    }
    
    /// <summary>
    /// Toggle the item on or off
    /// </summary>
    /// <param name="_canvasGroup"></param>
    /// <param name="_toggle"></param>
    /// <param name="OnComplete"></param>
    public static void Toggle(this CanvasGroup _canvasGroup, bool _toggle, Action OnComplete = null)
    {
        ToggleInteraction(_canvasGroup, _toggle);
    }

    /// <summary>
    /// Toggle an item to show it for a set amount of seconds before closing it
    /// </summary>
    /// <param name="_canvasGroup"></param>
    /// <param name="_toggle"></param>
    /// <param name="time"></param>
    /// <param name="duration"></param>
    /// <param name="OnMidway"></param>
    /// <param name="OnComplete"></param>
    public static void Toggle(this CanvasGroup _canvasGroup, bool _toggle, float time,float duration, Action OnMidway = null, Action OnComplete = null)
    {
        float from = _toggle ? 0 : 1;
        float to = _toggle ? 1 : 0;
    
        LeanTween.value(_canvasGroup.gameObject, f =>
        {
            _canvasGroup.alpha = f;
        }, from, to, time).setEaseOutSine().setOnComplete((() =>
        {
            OnMidway?.Invoke();
            LeanTween.delayedCall(_canvasGroup.gameObject, duration, () =>
            {
                LeanTween.value(_canvasGroup.gameObject, f =>
                {
                    _canvasGroup.alpha = f;
                }, to, from, time).setEaseInSine().setOnComplete(OnComplete);
            });
        }));
    
        ToggleInteraction(_canvasGroup, _toggle);
    }

    private static void ToggleInteraction(this CanvasGroup _canvasGroup, bool _toggle)
    {
        _canvasGroup.alpha = _toggle ? 1 : 0;
        _canvasGroup.interactable = _toggle;
        _canvasGroup.blocksRaycasts = _toggle;
    }
}


