using GridSystem;

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
        if (_tileSelector.CharacterOnTile(out Character _character))
        {
            EventManager.OnAbilityUsed?.Invoke(ability.Execute(caster, _character));
        }
    }
}
