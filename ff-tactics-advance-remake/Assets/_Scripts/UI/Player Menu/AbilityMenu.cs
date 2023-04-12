using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenu : PlayerMenu
{
    [field: SerializeField] public AbilityButton Button { get; private set; }
    [field: SerializeField] public Transform ButtonParent { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        OnComplete = DestroyButtons;
    }

    public void DestroyButtons()
    {
        menuButtons.Clear();
        
        foreach (Transform child in ButtonParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public override void Init()
    {
        menuButtons.Clear();
        
        foreach (var ability in GameManager.Instance.TurnManager.currentTurn.Character.EquippedAbilities)
        {
            var button = Instantiate(Button, ButtonParent);
            button.Init(ability);
            
            menuButtons.Add(button);
        }
        
        base.Init();
    }
}
