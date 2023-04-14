using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentHolderUI : MonoBehaviour
{
    [field: SerializeField] public TMP_Text EquipmentName { get; private set; }
    [field: SerializeField] public Image EquipmentIcon { get; private set; }

    public void Init(EquipmentData _equipment)
    {
        EquipmentName.text = _equipment.Name;
        EquipmentIcon.sprite = _equipment.Icon;
    }
    
    public void Reset()
    {
        EquipmentName.text = $"-";
        EquipmentIcon.sprite = null;
    }
}
