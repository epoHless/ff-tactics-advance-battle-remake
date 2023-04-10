using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    [field: SerializeField] public List<Character> Characters { get; private set; }
    [field: SerializeField] public TurnInformation currentTurn { get; set; }

    private Queue<Character> turnOrder = new Queue<Character>();

    private void Start()
    {
        StartTurn();
        EventManager.OnTurnChanged?.Invoke(currentTurn);
    }

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

    public bool IsTurnFinished(out Character _nextTurnCharacter)
    {
        if (currentTurn.HasMoved /*&& currentTurn.HasUsedAbilities*/) //todo add ability check
        {
            currentTurn = StartTurn();
            
            _nextTurnCharacter = currentTurn.Character;
            
            Debug.Log($"finished");
            
            return true;
        }
        else
        {
            _nextTurnCharacter = null;
            return false;
        }
    }
}
