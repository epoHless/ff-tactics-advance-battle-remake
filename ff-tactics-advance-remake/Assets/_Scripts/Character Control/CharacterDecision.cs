using UnityEngine;

[CreateAssetMenu(fileName = "Decision_", menuName = "Game/Controls/New Decision", order = 0)]
public abstract class CharacterDecision : ScriptableObject
{
    [field: SerializeField] public Character ControlledCharacter { get; private set; }

    public virtual void Init(Character _character)
    {
        ControlledCharacter = _character;
    }
    
    public virtual void MovementDecision(){}
    protected virtual void ActionDecision(){}

    public virtual void FaceDecision()
    {
        Debug.Log($"{ControlledCharacter.Data.Name} Has Finished!");

        if (!TurnManager.Instance.currentTurn.HasUsedAbilities)
        {
            ActionDecision();
            return;
        }else if (!TurnManager.Instance.currentTurn.HasMoved)
        {
            MovementDecision();
            return;
        }else
            TurnManager.Instance.StartTurn();
    }
}
