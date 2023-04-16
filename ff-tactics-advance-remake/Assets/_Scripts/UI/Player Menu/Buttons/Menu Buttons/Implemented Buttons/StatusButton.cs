using GridSystem;
using UnityEngine;

public class StatusButton : MenuButton
{
    [field: SerializeField] public int ID { get; private set; }
    
    protected override void ExecuteAction()
    {
        GameManager.Instance.statusState.ID = ID;
        GameManager.Instance.statusState.character = TurnManager.Instance.currentTurn.Character;
        GameManager.Instance.ChangeState(GameManager.Instance.statusState);
    }
}
