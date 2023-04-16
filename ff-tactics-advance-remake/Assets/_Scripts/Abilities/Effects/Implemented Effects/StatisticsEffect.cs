using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsEffect : AbilityEffect
{
    [SerializeField] private List<EStatType> statsToModify;
    [SerializeField] private float PercentageToModify;
    [SerializeField] private Vector3 offset;
    
    public override IEnumerator Execute(AbilityData abilityData, Character _caster, Character _target)
    {
        foreach (var statType in statsToModify)
        {
            if (_target.BattleStatistics.HasStatistic(statType, out var value))
            {
                yield return NotificationInfo.Instance.ShowInfo(_target.transform.position + offset, CombatSystem.ModifyStat(value, PercentageToModify), Color.yellow, $"{statType.ToString()}");
            }
        }
    }
}


public enum EStatType
{
    ATTACK = 1,
    DEFENSE = 2,
    MAGIC = 3,
    RESIST = 4,
    SPEED = 5
}