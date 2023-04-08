using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMenu : ToggablePanel
{
    [SerializeField] private List<MenuButton> menuButtons;

    public void Init()
    {
        EventSystem.current.SetSelectedGameObject(menuButtons[0].gameObject);
    }
}
