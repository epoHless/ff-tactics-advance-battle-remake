using System;
using FinalFantasy;
using GridSystem;
using UnityEngine;

public class MovementState : GameState
{
    private TileSelector tileSelector;
    public bool activateMovement = false;
    
    public MovementState(TileSelector _tileSelector)
    {
        tileSelector = _tileSelector;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);

        tileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position + Vector3.up * 0.5f;

        if (activateMovement) MovementManager.Instance.ActivateTilesInRange();
        tileSelector.ToggleSelector(true);
        
        InputSystem.EnableGridMovement();
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (_manager.TurnManager.currentTurn.HasMoved)
        {
            _manager.ChangeState(_manager.menuState);
            return;
        }
        
        if (InputSystem.WasBackPressed && !MovementManager.Instance.IsMoving)
        {
            _manager.ChangeState(_manager.menuState);
            return;
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        tileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position + Vector3.up * 0.5f;
        tileSelector.IsCharacterOnTile();
        
        tileSelector.ToggleSelector(false);
        
        MovementManager.Instance.DeactivateTilesInRange();
        
        InputSystem.DisableGridMovement();
    }
}
