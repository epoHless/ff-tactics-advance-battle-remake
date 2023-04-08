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

    public virtual LTDescr Toggle(bool _toggle, int _direction = 1)
    {
        var multiplier = _toggle ? 1 : -1;
        return rectTransform.LeanMoveX(_toggle ? 0 : rectTransform.rect.width * multiplier * _direction, .15f);
    }

    #endregion
}
