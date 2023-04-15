﻿using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPanel : ToggablePanel
{
    #region Properties

    [field: SerializeField] public GameObject ModelParent { get; private set; }
    [field: SerializeField] public TMP_Text Name { get; private set; }
    [field: SerializeField] public TMP_Text HP { get; private set; }
    [field: SerializeField] public TMP_Text MP { get; private set; }

    #endregion

    public List<GameObject> charModels = new List<GameObject>();
    private GameObject activeModel;
    
    #region Unity Methods

    protected override void Start()
    {
        base.Start();
        
        foreach (var character in TurnManager.Instance.Characters)
        {
            var charModel = Instantiate(character.Data.Model, ModelParent.transform);

            charModel.name = character.Data.Name;
            
            charModel.transform.localScale = Vector3.one * 300f;

            charModel.layer = 7;
            charModel.transform.GetChild(1).GetChild(0).gameObject.layer = 7;
            var body = charModel.transform.GetChild(1).GetChild(1).gameObject;
            body.GetComponent<SkinnedMeshRenderer>().material = character.Data.CharacterMaterial;
            body.layer = 7;
            
            charModel.SetActive(false);
            charModels.Add(charModel);
        }
    }

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

    private void HidePanel(Character _character)
    {
        Toggle(false).setOnComplete(() =>
        {
            if (!activeModel) return;
            activeModel.SetActive(false);
        });
    }

    private void DisplayCharacterInfo(Character _character)
    {
        if(activeModel) activeModel.SetActive(false);
        
        activeModel = charModels.Find(o =>
        {
            if (o.name == _character.Data.Name)
            {
                o.SetActive(true);
                return true;
            }
            else
            {
                o.SetActive(false);
                return false;
            }
        });
        
        Name.text = _character.Data.Name;
        HP.text = $"{_character.BattleStatistics.CurrentHP}/ {_character.BattleStatistics.HP}";
        MP.text = $"{_character.BattleStatistics.CurrentMP}/ {_character.BattleStatistics.MP}";
        
        Toggle(true);
    }

    #endregion
}
