using System;

public static class CombatSystem
{
    public static float DealDamage(AbilityData abilityData, Character _caster, Character _target)
    {
        float baseAttack = (abilityData.AbilityType) switch 
        {
            EAbilityType.PHYSICAL => _caster.BattleStatistics.Attack.Value,
            EAbilityType.MAGICAL => _caster.BattleStatistics.Defense.Value,
            _ => throw new ArgumentOutOfRangeException()
        };

        float baseDefense = (abilityData.AbilityType) switch
        {
            EAbilityType.PHYSICAL => _target.BattleStatistics.Defense.Value,
            EAbilityType.MAGICAL => _target.BattleStatistics.Resist.Value,
            _ => throw new ArgumentOutOfRangeException()
        };

        var damage = baseAttack - (baseDefense / 2);

        damage = (abilityData.AbilityPower * damage) * 0.1f;

        _target.BattleStatistics.CurrentHP.Value -= damage;

        if (_target.BattleStatistics.CurrentHP.Value <= 0)
        {
            _target.BattleStatistics.CurrentHP.Value = 0;
            EventManager.OnCharacterDeath?.Invoke(_target);
        }
        
        return damage;
    }
    
    public static float Heal(AbilityData abilityData, Character _caster, Character _target)
    {
        float baseAttack = (abilityData.AbilityType) switch 
        {
            EAbilityType.PHYSICAL => _caster.BattleStatistics.Attack.Value,
            EAbilityType.MAGICAL => _caster.BattleStatistics.Defense.Value,
            _ => throw new ArgumentOutOfRangeException()
        };

        float baseDefense = (abilityData.AbilityType) switch
        {
            EAbilityType.PHYSICAL => _target.BattleStatistics.Defense.Value,
            EAbilityType.MAGICAL => _target.BattleStatistics.Resist.Value,
            _ => throw new ArgumentOutOfRangeException()
        };

        var heal = baseAttack - (baseDefense / 2);

        heal = (abilityData.AbilityPower * heal) * 0.1f;

        _target.BattleStatistics.CurrentHP.Value += heal;

        if (_target.BattleStatistics.CurrentHP.Value >= _target.BattleStatistics.HP.Value)
        {
            _target.BattleStatistics.CurrentHP.Value = _target.BattleStatistics.HP.Value;
        }
        
        return heal;
    }

    public static float ModifyStat(StatisticValue _value, float _percentage)
    {
        _value.Value = _value.Value * (1 + _percentage / 100);
        return _value.Value;
    }
}
