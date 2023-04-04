using System;
using UnityEngine;

public class ToggablePanel : MonoBehaviour
{
    #region Fields

    private RectTransform rectTransform;

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    #endregion

    #region Methods

    protected virtual void Toggle(bool _toggle)
    {
        var multiplier = _toggle ? 1 : -1;
        rectTransform.LeanMoveX(_toggle ? 0 : rectTransform.rect.width * multiplier, .15f);
    }

    #endregion
}
