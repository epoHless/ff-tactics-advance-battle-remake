using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    #region Fields

    private Queue<Character> turnOrder = new Queue<Character>();

    #endregion

    #region Properties

    [field: SerializeField] public List<Character> Characters { get; private set; }
    [field: SerializeField] public TurnInformation currentTurn { get; set; }

    #endregion

    #region Unity Methods

    private void Start()
    {
        StartTurn();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        EventManager.OnAbilityFinished += CheckAbilityAsUsed;
        EventManager.OnCharacterDeath += RemoveCharacterFromTurns;
    }

    private void OnDisable()
    {
        EventManager.OnAbilityFinished -= CheckAbilityAsUsed;
        EventManager.OnCharacterDeath -= RemoveCharacterFromTurns;
    }

    #endregion

    #region Methods

    public TurnInformation StartTurn()
    {
        if (turnOrder.Count <= 0)
        {
            turnOrder = new Queue<Character>(Characters.OrderBy(character => character.BattleStatistics.Speed).ToList()); //todo add Wait priority
        }
        
        EventManager.OnTurnChanged?.Invoke(currentTurn);

        currentTurn = new TurnInformation(turnOrder.Dequeue());
        return currentTurn;
    }

    #endregion

    #region Ability Event Methods

    private void CheckAbilityAsUsed()
    {
        currentTurn.HasUsedAbilities = true;
    }

    #endregion

    #region Character Events

    private void RemoveCharacterFromTurns(Character _character)
    {
        Characters.Remove(_character);
        
        if (turnOrder.Contains(_character))
        {
            turnOrder = new Queue<Character>(turnOrder.Where(character => character != _character));
        }
    }

    #endregion
}
