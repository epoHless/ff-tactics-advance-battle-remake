using System;

public static class CombatSystem
{
    public static void DealDamage(AbilityData abilityData, Character _caster, Character _target)
    {
        float baseAttack = (abilityData.AbilityType) switch //todo add atk bonus from items
        {
            EAbilityType.PHYSICAL => _caster.BattleStatistics.Attack,
            EAbilityType.MAGICAL => _caster.BattleStatistics.Defense,
            _ => throw new ArgumentOutOfRangeException()
        };

        float baseDefense = (abilityData.AbilityType) switch //todo add def bonus from items
        {
            EAbilityType.PHYSICAL => _target.BattleStatistics.Defense,
            EAbilityType.MAGICAL => _target.BattleStatistics.Resist,
            _ => throw new ArgumentOutOfRangeException()
        };

        var damage = baseAttack - (baseDefense / 2);

        damage = (abilityData.AbilityPower * damage) * 0.1f;

    }
}
