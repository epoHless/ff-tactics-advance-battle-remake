using System;
using FinalFantasy;
using GridSystem;
using UnityEngine;

public class MovementState : GameState
{
    private TileSelector tileSelector;
    public bool IsMovementActive = false;
    
    public MovementState(TileSelector _tileSelector)
    {
        tileSelector = _tileSelector;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        tileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position;

        if (IsMovementActive) MovementManager.Instance.ActivateTilesInRange(_manager.TurnManager.currentTurn.Character.Movement.MovementData.Range);
        else
        {
            _manager.StatusMenu.gameObject.SetActive(true);
            _manager.PlayerMenu.SwitchPanel(_manager.StatusMenu);
            _manager.StatusMenu.Init();
        }
        
        tileSelector.ToggleSelector(true);
        
        InputSystem.EnableGridMovement();
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);
        
        if (InputSystem.WasBackPressed && !MovementManager.Instance.IsMoving)
        {
            _manager.ChangeState(_manager.menuState);
            return;
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        tileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position;
        tileSelector.IsCharacterOnTile();
        
        tileSelector.ToggleSelector(false);

        if (PlayerMenu.ActiveMenu == _manager.StatusMenu)
        {
            _manager.StatusMenu.gameObject.SetActive(false);
            // _manager.StatusMenu.SwitchPanel(_manager.PlayerMenu);
        }
        
        MovementManager.Instance.DeactivateTilesInRange(_manager.TurnManager.currentTurn.Character.Movement.MovementData.Range);
        
        InputSystem.DisableGridMovement();
    }
}
