using GridSystem;
using UnityEngine;

public class StatusButton : MenuButton
{
    [field: SerializeField] public int ID { get; private set; }
    
    protected override void ExecuteAction()
    {
        if (!TileSelector.CurrentCharacter) return;
        GameManager.Instance.statusState.ID = ID;
        GameManager.Instance.statusState.character = TileSelector.CurrentCharacter;
        GameManager.Instance.ChangeState(GameManager.Instance.statusState);
    }
}
