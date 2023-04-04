using UnityEngine;

[CreateAssetMenu(fileName = "Statistics_", menuName = "Characters/New Base Statistics", order = 0)]
public class StatisticsData : ScriptableObject
{
    [field: SerializeField] public float HP { get; private set; }
    [field: SerializeField] public float MP { get; private set; }
    [field: SerializeField] public float Attack { get; private set; }
    [field: SerializeField] public float Defense { get; private set; }
    [field: SerializeField] public float Magic { get; private set; }
    [field: SerializeField] public float Resist { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    public BattleStatistics Clone => new BattleStatistics(this);
}
