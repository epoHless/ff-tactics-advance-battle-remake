using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    private Button button;

    public bool Enabled => button.enabled;
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ExecuteAction);
    }

    protected virtual void ExecuteAction() { }
}
