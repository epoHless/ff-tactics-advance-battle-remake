using FinalFantasy;
using GridSystem;
using UnityEngine;

public class MovementState : GameState
{
    private TileSelector tileSelector;

    public MovementState(TileSelector _tileSelector)
    {
        tileSelector = _tileSelector;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);

        tileSelector.transform.position = MovementManager.Instance.Character.transform.position + Vector3.up * 0.5f;
        tileSelector.ToggleSelector(true);
        
        MovementManager.Instance.ActivateTilesInRange();
        
        InputSystem.EnableGridMovement();
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed)
        {
            _manager.ChangeState(_manager.menuState);
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        tileSelector.transform.position = MovementManager.Instance.Character.transform.position + Vector3.up * 0.5f;
        tileSelector.IsCharacterOnTile();
        
        tileSelector.ToggleSelector(false);
        
        InputSystem.DisableGridMovement();
    }
}
