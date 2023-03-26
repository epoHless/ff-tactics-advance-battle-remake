using UnityEngine;

[CreateAssetMenu(fileName = "MovementData_", menuName = "Characters/Data/New Movement Data", order = 0)]
public class MovementData : ScriptableObject
{
    #region Properties

    [field: SerializeField] public int Range { get; private set; }
    [field: SerializeField] public int JumpHeight { get; private set; }

    #endregion
}
