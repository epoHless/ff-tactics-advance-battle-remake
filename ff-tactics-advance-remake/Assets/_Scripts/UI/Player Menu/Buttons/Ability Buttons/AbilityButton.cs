﻿using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MenuButton
{
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityCost;
    
    private AbilityData associatedAbility;

    public void Init(AbilityData _ability)
    {
        button = GetComponent<Button>();
        
        associatedAbility = _ability;
        
        abilityName.text += associatedAbility.Name;
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