using UnityEngine;

[System.Serializable]
public class BattleStatistics
{
    #region Properties

    [field: SerializeField] public float HP { get; private set; }
    
    [field: SerializeField] public float CurrentHP { get; set; }
    [field: SerializeField] public float MP { get; private set; }
    
    [field: SerializeField] public float CurrentMP { get; set; }
    [field: SerializeField] public float Attack { get; private set; }
    [field: SerializeField] public float Defense { get; private set; }
    [field: SerializeField] public float Magic { get; private set; }
    [field: SerializeField] public float Resist { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    
    #endregion

    #region Constructor

    public BattleStatistics(StatisticsData _statistics)
    {
        HP = _statistics.HP;
        MP = _statistics.MP;
        Attack = _statistics.Attack;
        Defense = _statistics.Defense;
        Magic = _statistics.Magic;
        Resist = _statistics.Resist;
        Speed = _statistics.Speed;

        CurrentHP = HP;
        CurrentMP = MP;
    }

    #endregion
}
