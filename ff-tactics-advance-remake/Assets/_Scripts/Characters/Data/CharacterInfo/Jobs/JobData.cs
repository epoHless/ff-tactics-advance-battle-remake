using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job_", menuName = "Characters/New Job", order = 0)]
public class JobData : ScriptableObject
{
    #region Properties

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public List<JobRequirement> Branches { get; private set; }
    [field: SerializeField] public StatisticsData BaseStatistics { get; private set; }
    [field: SerializeField] public List<EquipmentCategoryData> EquippableCategories { get; private set; }
    
    [field: SerializeField] public AbilityData BaseAttack { get; private set; }

    #endregion

    #region Methods

    public bool CanChangeJob(int _abilitiesMastered, out JobRequirement _job) //todo check from character's abilities and swap _abilitiesMastered for it
    {
        var job = Branches.Find(requirement => _abilitiesMastered >= requirement.AbilitiesRequired);
        
        if (job != null)
        {
            _job = job;
            return true;
        }
        else
        {
            _job = null;
            return false;
        }
    }

    #endregion
}
