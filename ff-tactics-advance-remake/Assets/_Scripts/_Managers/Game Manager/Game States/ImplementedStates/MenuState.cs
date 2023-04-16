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

        
        CameraManager.Instance.ToggleCamera(ECameraType.TOPDOWN);
        CameraManager.Instance.SetFollowObject(_manager.TileSelector.transform);

        if (_manager.TurnManager.currentTurn.HasMoved && _manager.TurnManager.currentTurn.HasUsedAbilities)
        {
            _manager.ChangeState(_manager.facingDirectionState);
        }
        else
        {
            _manager.TileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position;
            _manager.TileSelector.IsCharacterOnTile();
            _manager.TileSelector.ToggleSelector(true);

            if (_manager.TurnManager.currentTurn.Character.AIDecision)
            {
                _manager.TurnManager.currentTurn.Character.AIDecision.Init(_manager.TurnManager.currentTurn.Character);
                _manager.TurnManager.currentTurn.Character.AIDecision.FaceDecision();
            }
            else
            {
                menu.gameObject.SetActive(true);
                menu.Init();
                menu.Toggle(true);
            }
        }
        
        EventManager.OnCharacterHovered?.Invoke(TurnManager.Instance.currentTurn.Character);
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed && PlayerMenu.ActiveMenu.PreviousPanel)
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
        
        _manager.TileSelector.ToggleSelector(false);

        menu.Toggle(false, -1).setOnComplete(() =>
        {
            menu.gameObject.SetActive(false);
            InputSystem.EnableConfirm();
        });
    }
}
