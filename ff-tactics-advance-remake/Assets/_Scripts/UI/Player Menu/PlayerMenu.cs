using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMenu : ToggablePanel
{
    [field: SerializeField] public bool toggleOnStart { get; private set; }
    [field: SerializeField] public PlayerMenu PreviousPanel { get; private set; }
    [field: SerializeField] public List<MenuButton> menuButtons { get; private set; }

    public static PlayerMenu ActiveMenu;
    
    private void Start()
    {
        gameObject.SetActive(toggleOnStart);
        
        if(toggleOnStart) Init();
    }

    public virtual void Init()
    {
        if (menuButtons.Count > 0)
        {
            foreach (var button in menuButtons)
            {
                button.Enabled = button.CanBeEnabled();
            }
            
            EventSystem.current.SetSelectedGameObject(menuButtons[0].gameObject);
        }
        
        ActiveMenu = this;
    }

    public override void SwitchPanel(ToggablePanel _next)
    {
        if (menuButtons.Count > 0)
        {
            foreach (var button in menuButtons)
            {
                button.Enabled = false;
            }
        }

        base.SwitchPanel(_next);
    }
}
