using System;
using FinalFantasy;
using UnityEngine;

public class MenuState : GameState
{
    private PlayerMenu menu;
    
    public MenuState(PlayerMenu _menu)
    {
        menu = _menu;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        InputSystem.DisableGameInput();
        InputSystem.DisableConfirm();

        if (_manager.TurnManager.currentTurn.HasMoved && _manager.TurnManager.currentTurn.HasUsedAbilities)
        {
            _manager.ChangeState(_manager.facingDirectionState);
        }
        else
        {
            _manager.tileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position;
            _manager.tileSelector.IsCharacterOnTile();
            _manager.tileSelector.ToggleSelector(true);
        
            menu.gameObject.SetActive(true);

            menu.Init();
            menu.Toggle(true);
        }
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed && PlayerMenu.ActiveMenu.PreviousPanel && !PlayerMenu.ActiveMenu.toggleOnStart)
        {
            PlayerMenu.ActiveMenu.SwitchPanel(PlayerMenu.ActiveMenu.PreviousPanel);
            PlayerMenu.ActiveMenu.PreviousPanel.Init();
        }
        else if (InputSystem.WasBackPressed)
        {
            //goes to map exploration
            _manager.movementState.IsMovementActive = false;
            _manager.ChangeState(_manager.movementState);
            return;
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        _manager.tileSelector.ToggleSelector(false);

        menu.Toggle(false, -1).setOnComplete(() =>
        {
            menu.gameObject.SetActive(false);
            InputSystem.EnableConfirm();
        });
    }
}
