using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Race_", menuName = "Characters/New Race", order = 0)]
public class RaceData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public List<JobData> BaseJobs;
}
