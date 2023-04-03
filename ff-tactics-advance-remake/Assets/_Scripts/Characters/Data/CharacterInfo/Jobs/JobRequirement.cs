using UnityEngine;

[System.Serializable]
public class JobRequirement
{
    #region Properties

    [field: SerializeField] public JobData Job { get; private set; }
    [field: SerializeField] public int AbilitiesRequired { get; private set; }

    #endregion
}
