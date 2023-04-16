using FinalFantasy;
using GridSystem;
using UnityEngine;

public class TargetCommand : SelectionCommand
{
    private Character caster;
    private AbilityData ability;

    public TargetCommand(Character _caster, AbilityData _ability)
    {
        caster = _caster;
        ability = _ability;
    }
    
    public override void Execute(TileSelector _tileSelector)
    {
        if (PathFinder.CharacterOnTile(_tileSelector.CurrentTile, out Character _character))
        {
            if (!_character.IsDead)
            {
                if (caster.BattleStatistics.CurrentMP >= ability.ManaCost)
                {
                    EventManager.OnAbilityUsed?.Invoke(ability.Execute(caster, _character));
                }
            
                InputSystem.DisableGameInput();
                InputSystem.DisableConfirm();
            }
        }
    }
}
