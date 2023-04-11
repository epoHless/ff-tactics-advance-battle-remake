using UnityEngine;

[CreateAssetMenu(fileName = "Equipment_", menuName = "Game/Equipment/New Equipment")]
public class EquipmentData : ScriptableObject
{
    [field: SerializeField] public EquipmentCategoryData CategoryID { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public ElementData Element { get; private set; }
    [field: SerializeField] public AbilityData Ability { get; private set; }
    [field: SerializeField] public EffectData Effect { get; private set; }
    [field: SerializeField] public BattleStatistics Statistics { get; private set; }

    public bool EquipItem(Character _character)
    {
        if (_character.Equipment.Exists(data => data.CategoryID == CategoryID) || 
            _character.Equipment.Count >= 5 ||
            !_character.CurrentJob.EquippableCategories.Exists(data => data == CategoryID && data.ID == CategoryID.ID)) return false;
        else
        {
            _character.Equipment.Add(this);
            EventManager.OnItemEquipped?.Invoke(this);
            
            if (!_character.EquippedAbilities.Contains(Ability))
            {
                _character.EquippedAbilities.Add(Ability);
                EventManager.OnAbilityEquipped?.Invoke(Ability);
            }

            return true;
        }
    }
}
