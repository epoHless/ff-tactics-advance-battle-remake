using System;
using UnityEngine;

public class ToggablePanel : MonoBehaviour
{
    #region Fields

    private RectTransform rectTransform;

    protected Action OnComplete = null;
    
    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    #endregion

    #region Methods

    public virtual LTDescr Toggle(bool _toggle, int _direction = 1) //if _direction == 1 = move left
    {
        var multiplier = _toggle ? 1 : -1;
        return rectTransform.LeanMoveX(_toggle ? 0 : rectTransform.rect.width * multiplier * _direction, .15f);
    }

    public virtual void SwitchPanel(ToggablePanel _next)
    {
        if (!_next) return;
        
        Toggle(false, -1).setOnComplete((() =>
        {
            OnComplete?.Invoke();
            _next.gameObject.SetActive(true);
            _next.Toggle(true);
        }));
    }

    #endregion
}
