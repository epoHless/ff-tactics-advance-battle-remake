using UnityEngine.UI;

public class AttackAbilityButton : AbilityButton
{
    private void OnEnable()
    {
        EventManager.OnCharacterHovered += UpdateInfo;

        if (TurnManager.Instance)
        {
            Init(TurnManager.Instance.currentTurn.Character.CurrentJob.BaseAttack);
        }
    }

    private void OnDisable()
    {
        EventManager.OnCharacterHovered -= UpdateInfo;
    }

    public override void Init(AbilityData _ability)
    {
        button = GetComponent<Button>();
        associatedAbility = _ability;
        abilityName.text = _ability.Name;
    }

    private void UpdateInfo(Character character)
    {
        Init(character.CurrentJob.BaseAttack);
    }
}
