using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour, ISubmitHandler
{
    protected Button button;

    public bool Enabled
    {
        get => button.interactable;
        set => button.interactable = value;
    }
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    protected virtual void ExecuteAction() { }
    
    public void OnSubmit(BaseEventData eventData)
    {
        ExecuteAction();
    }
}
