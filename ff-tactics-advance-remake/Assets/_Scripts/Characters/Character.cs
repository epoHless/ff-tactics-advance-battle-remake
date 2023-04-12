using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Properties

    [field: SerializeField, SORender] public CharacterData Data { get; private set; }
    [field: SerializeField, SORender] public JobData CurrentJob { get; private set; }
    [field: SerializeField, SORender] public CharacterMovement Movement { get; private set; }
    [field: SerializeField] public BattleStatistics BattleStatistics { get; set; }
    [field: SerializeField] public List<EquipmentData> Equipment { get; private set; }

    public List<AbilityData> EquippedAbilities { get; private set; } = new List<AbilityData>();

    #endregion

    #region Unity Methods

    private void Awake()
    {
        BattleStatistics = CurrentJob.BaseStatistics.Clone;
    }

    private void Start()
    {
        foreach (var data in Equipment)
        {
            BattleStatistics += data.Statistics;
        }

        foreach (var data in Equipment)
        {
            EquippedAbilities.Add(data.Ability);
        }
    }

    #endregion

    #region Methods

    public bool HasNeighbors(out List<Character> _neighbors)
    {
        _neighbors = new List<Character>();
        
        var directions = new List<Vector3>()
        {
            Vector3.forward, Vector3.back, Vector3.left, Vector3.right
        };

        foreach (var direction in directions)
        {
            if (GetNeighbor(direction, out Character _neighbor))
            {
                _neighbors.Add(_neighbor);
            }
        }
        
        return _neighbors.Count > 0;
    }

    private bool GetNeighbor(Vector3 direction, out Character _neighbor)
    {
        var position = transform.position;
            
        Ray ray = new Ray(position, direction);
            
        if (Physics.Raycast(ray, out RaycastHit hit, 1f))
        {
            if (hit.transform.TryGetComponent(out Character _character))
            {
                _neighbor = _character;
                return true;
            }
        }

        _neighbor = null;
        return false;
    }
    
    #endregion
}