using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MenuButton
{
    [SerializeField] protected TMP_Text abilityName;
    [SerializeField] protected TMP_Text abilityCost;
    
    [field: SerializeField] protected virtual AbilityData associatedAbility { get; set; }

    public virtual void Init(AbilityData _ability)
    {
        button = GetComponent<Button>();
        
        associatedAbility = _ability;
        
        abilityName.text += _ability;
        abilityCost.text = $"{associatedAbility.ManaCost.ToString()} mp" ;
    }

    protected override void ExecuteAction()
    {
        EventManager.OnCommandSent?.Invoke(new TargetCommand(TurnManager.Instance.currentTurn.Character, associatedAbility));
        
        GameManager.Instance.targetSelectionState.Init(associatedAbility);
        GameManager.Instance.ChangeState(GameManager.Instance.targetSelectionState);

        PlayerMenu.ActiveMenu.Toggle(false, -1);

        if (PlayerMenu.ActiveMenu is AbilityMenu menu)
        {
            menu.DestroyButtons();
        }
    }
}
