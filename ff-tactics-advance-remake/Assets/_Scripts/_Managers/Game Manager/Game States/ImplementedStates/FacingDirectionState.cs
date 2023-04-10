using System;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

public class FacingDirectionState : GameState
{
    private FacingDirectionHolder facingDirectionHolder;
    
    public FacingDirectionState(FacingDirectionHolder _facingDirectionHolder)
    {
        facingDirectionHolder = _facingDirectionHolder;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        facingDirectionHolder.gameObject.SetActive(true);
        facingDirectionHolder.Init(_manager.TurnManager.currentTurn.Character.transform.position + Vector3.up * 1.4f);
        
        InputSystem.EnableFacingDirection();
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed && !_manager.TurnManager.currentTurn.HasMoved)
        {
            _manager.ChangeState(_manager.menuState);
            return;
        }
        
        if (InputSystem.WasConfirmPressed)
        {
            Debug.Log($"PRESSED CONFIRM ON FACING DIRECTION!");
            _manager.ChangeState(_manager.menuState);
            return;
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        InputSystem.DisableFacingDirection();

        _manager.TurnManager.StartTurn();
        
        facingDirectionHolder.gameObject.SetActive(false);
    }
}
