using FinalFantasy;
using GridSystem;
using UnityEngine;

public class TargetSelectionState : GameState
{
    private TileSelector tileSelector;
    private AbilityData ability;
    
    public TargetSelectionState(TileSelector _tileSelector)
    {
        tileSelector = _tileSelector;
    }

    public void Init(AbilityData _ability)
    {
        ability = _ability;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        tileSelector.transform.position = _manager.TurnManager.currentTurn.Character.transform.position;

        MovementManager.Instance.ActivateTilesInRange(ability.AbilityRange);
        tileSelector.ToggleSelector(true);
        
        InputSystem.EnableGridMovement();
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (TurnManager.Instance.currentTurn.HasUsedAbilities)
        {
            _manager.ChangeState(_manager.menuState);
            return;
        }
        
        if (InputSystem.WasBackPressed && !AbilitiesProcessor.IsCasting)
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
        
        MovementManager.Instance.DeactivateTilesInRange(ability.AbilityRange);
        
        InputSystem.DisableGridMovement();
    }
}
