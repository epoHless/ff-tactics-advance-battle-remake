using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsPanel : PlayerMenu
{
    [SerializeField] private TMP_Text wAtk;
    [SerializeField] private TMP_Text wDef;
    [SerializeField] private TMP_Text mAtk;
    [SerializeField] private TMP_Text mDef;
    [SerializeField] private TMP_Text move;
    [SerializeField] private TMP_Text jump;
    
    [SerializeField] private TMP_Text charName;
    [SerializeField] private TMP_Text job;

    [SerializeField] private List<EquipmentHolderUI> EquipmentHolderUis;

    public void Init(Character _character)
    {
        wAtk.text = $"Weapon ATK {_character.BattleStatistics.Attack.Value}";
        wDef.text = $"Weapon DEF {_character.BattleStatistics.Defense.Value}";
        mAtk.text = $"Magic ATK {_character.BattleStatistics.Magic.Value}";
        mDef.text = $"Magic DEF {_character.BattleStatistics.Resist.Value}";
        move.text = $"Move {_character.Movement.MovementData.Range}";
        jump.text = $"Jump {_character.Movement.MovementData.JumpHeight}";

        charName.text = _character.Data.Name;
        job.text = _character.CurrentJob.Name;

        for (int i = 0; i < EquipmentHolderUis.Count; i++)
        {
            if (i < _character.Equipment.Count)
            {
                EquipmentHolderUis[i].Init(_character.Equipment[i]);
            }
            else
            {
                EquipmentHolderUis[i].Reset();
            }
        }
        
        Init();
    }
}
