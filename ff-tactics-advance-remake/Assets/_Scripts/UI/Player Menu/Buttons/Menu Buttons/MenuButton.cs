using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour, ISubmitHandler
{
    #region Fields

    protected Button button;

    #endregion

    #region Properties

    public bool Enabled
    {
        get => button.interactable;
        set => button.interactable = value;
    }

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }

    #endregion

    #region Methods

    public virtual bool CanBeEnabled()
    {
        return true; 
    }
    
    protected virtual void ExecuteAction() { }
    
    public void OnSubmit(BaseEventData eventData)
    {
        if(Enabled) ExecuteAction();
    }

    #endregion

    
}
