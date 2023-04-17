using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

public class PauseMenu : ToggablePanel
{
    private bool isOpen = false;
    [SerializeField] private PauseButton pauseButton;

    protected override void Awake()
    {
        base.Awake();
        pauseButton = GetComponentInChildren<PauseButton>();
    }

    protected override void Start()
    {
        base.Start();
        Toggle(false, -1);
    }

    private void OnEnable()
    {
        InputSystem.AddMenuListener(ToggleMenu);
    }

    private void OnDisable()
    {
        InputSystem.RemoveMenuListener(ToggleMenu);
    }

    private void ToggleMenu(InputAction.CallbackContext obj)
    {
        isOpen = !isOpen;

        Toggle(isOpen, isOpen ? 1 : -1);
        EventSystem.current.SetSelectedGameObject(isOpen ? pauseButton.gameObject : EventSystem.current.firstSelectedGameObject);
    }
}
