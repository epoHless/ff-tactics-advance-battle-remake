using System;

public static class CombatSystem
{
    public static float DealDamage(AbilityData abilityData, Character _caster, Character _target)
    {
        float baseAttack = (abilityData.AbilityType) switch 
        {
            EAbilityType.PHYSICAL => _caster.BattleStatistics.Attack,
            EAbilityType.MAGICAL => _caster.BattleStatistics.Defense,
            _ => throw new ArgumentOutOfRangeException()
        };

        float baseDefense = (abilityData.AbilityType) switch
        {
            EAbilityType.PHYSICAL => _target.BattleStatistics.Defense,
            EAbilityType.MAGICAL => _target.BattleStatistics.Resist,
            _ => throw new ArgumentOutOfRangeException()
        };

        var damage = baseAttack - (baseDefense / 2);

        damage = (abilityData.AbilityPower * damage) * 0.1f;

        _target.BattleStatistics.CurrentHP -= damage;

        if (_target.BattleStatistics.CurrentHP <= 0)
        {
            EventManager.OnCharacterDeath?.Invoke(_target);
        }
        
        return damage;
    }
}
