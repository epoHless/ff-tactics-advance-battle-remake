using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackAbilityButton : AbilityButton
{
    private void OnEnable()
    {
        EventManager.OnTurnChanged += UpdateInfo;
    }

    private void OnDisable()
    {
        EventManager.OnTurnChanged -= UpdateInfo;
    }

    public override void Init(AbilityData _ability)
    {
        button = GetComponent<Button>();
        associatedAbility = _ability;
        abilityName.text = _ability.Name;
    }

    private void UpdateInfo(TurnInformation _item)
    {
        Init(_item.Character.CurrentJob.BaseAttack);
    }
}
