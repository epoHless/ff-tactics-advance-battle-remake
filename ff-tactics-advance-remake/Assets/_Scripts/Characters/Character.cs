using UnityEngine;

public class Character : MonoBehaviour
{
    #region Properties

    [field: SerializeField, SORender] public CharacterData Data { get; private set; }
    [field: SerializeField, SORender] public JobData CurrentJob { get; private set; }
    [field: SerializeField, SORender] public CharacterMovement Movement { get; private set; }
    [field: SerializeField] public BattleStatistics BattleStatistics { get; set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        BattleStatistics = CurrentJob.BaseStatistics.Clone;
    }

    #endregion
}