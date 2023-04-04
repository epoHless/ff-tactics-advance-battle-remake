using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPanel : ToggablePanel
{
    #region Properties

    [field: SerializeField] public Image Icon { get; private set; }
    [field: SerializeField] public TMP_Text Name { get; private set; }
    [field: SerializeField] public TMP_Text HP { get; private set; }
    [field: SerializeField] public TMP_Text MP { get; private set; }

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.OnCharacterHovered += DisplayCharacterInfo;
        EventManager.OnCharacterUnhovered += HidePanel;
    }
    
    private void OnDisable()
    {
        EventManager.OnCharacterHovered -= DisplayCharacterInfo;
        EventManager.OnCharacterUnhovered -= HidePanel;
    }

    #endregion

    #region Character Info Methods

    private void HidePanel()
    {
        Toggle(false);
    }

    private void DisplayCharacterInfo(Character _character)
    {
        Icon.sprite = _character.Data.Icon;
        Name.text = _character.Data.Name;
        HP.text = $"{_character.BattleStatistics.CurrentHP}/ {_character.BattleStatistics.HP}";
        MP.text = $"{_character.BattleStatistics.CurrentMP}/ {_character.BattleStatistics.MP}";
        
        Toggle(true);
    }

    #endregion
    

    
}
