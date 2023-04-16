using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleStatistics
{
    #region Properties

    [field: SerializeField] public StatisticValue HP { get; private set; } = new StatisticValue();
    [field: SerializeField] public StatisticValue CurrentHP { get; set; } = new StatisticValue();
    
    [field: SerializeField] public StatisticValue MP { get; private set; } = new StatisticValue();
    [field: SerializeField] public StatisticValue CurrentMP { get; set; } = new StatisticValue();
    
    [field: SerializeField] public StatisticValue Attack { get; private set; } = new StatisticValue(1);
    [field: SerializeField] public StatisticValue Defense { get; private set; } = new StatisticValue(2);
    
    [field: SerializeField] public StatisticValue Magic { get; private set; } = new StatisticValue(3);
    [field: SerializeField] public StatisticValue Resist { get; private set; } = new StatisticValue(4);
    
    [field: SerializeField] public StatisticValue Speed { get; private set; } = new StatisticValue(5);

    public List<StatisticValue> Stats;

    #endregion

    #region Constructor

    public BattleStatistics()
    {
    }
    
    public BattleStatistics(StatisticsData _statistics)
    {
        HP.Value = _statistics.HP;
        MP.Value = _statistics.MP;
        Attack.Value = _statistics.Attack;
        Defense.Value = _statistics.Defense;
        Magic.Value = _statistics.Magic;
        Resist.Value = _statistics.Resist;
        Speed.Value = _statistics.Speed;

        CurrentHP.Value = HP.Value;
        CurrentMP.Value = MP.Value;
    }

    #endregion

    #region Methods

    public bool HasStatistic(EStatType _type, out StatisticValue _value)
    {
        _value = Stats.Find(value => value.ID == (int)_type);
        return _value != null;
    }

    public void Init()
    {
        Stats = new List<StatisticValue>
        {
            Attack, Defense, Magic, Resist, Speed
        };
    }

    #endregion
    
    #region Overloads

    public static BattleStatistics operator + (BattleStatistics _left, BattleStatistics _right)
    {
        var stats = new BattleStatistics
        {
            Attack = _left.Attack + _right.Attack,
            Defense = _left.Defense + _right.Defense,
            HP = _left.HP + _right.HP,
            MP = _left.MP + _right.MP,
            Resist = _left.Resist + _right.Resist,
            Speed = _left.Speed + _right.Speed,
            Magic = _left.Magic + _right.Magic,
        };

        stats.Attack.ID = 1;
        stats.Defense.ID = 2;
        stats.Magic.ID = 3;
        stats.Resist.ID = 4;
        stats.Speed.ID = 5;
        
        stats.CurrentHP.Value = stats.HP.Value;
        stats.CurrentMP.Value = stats.MP.Value;

        return stats;
    } 
    
    public static BattleStatistics operator - (BattleStatistics _left, BattleStatistics _right)
    {
        var stats = new BattleStatistics
        {
            Attack = _left.Attack - _right.Attack,
            Defense = _left.Defense - _right.Defense,
            HP = _left.HP - _right.HP,
            MP = _left.MP - _right.MP,
            Resist = _left.Resist - _right.Resist,
            Speed = _left.Speed - _right.Speed,
            Magic = _left.Magic - _right.Magic,
        };
    
        stats.CurrentHP.Value = stats.HP.Value;
        stats.CurrentMP.Value = stats.MP.Value;
    
        return stats;
    } 

    #endregion
}
