using GridSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField, SORender] public CharacterData Data { get; private set; }
    [field: SerializeField, SORender] public JobData CurrentJob { get; private set; }
    [field: SerializeField, SORender] public StatisticsData Statistics { get; private set; }
    [field: SerializeField, SORender] public CharacterMovement Movement { get; private set; }
}
