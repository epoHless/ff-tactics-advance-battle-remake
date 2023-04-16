using System.Collections;
using GridSystem;
using UnityEngine;

public static class EventManager
{
    #region Delegates

    public delegate void Evt();
    public delegate void Evt<T>(T _item);
    public delegate void Evt<T1, T2>(T1 _item1, T2 _item2);
    public delegate void Evt<T1, T2, T3>(T1 _item1, T2 _item2, T3 _item3);

    #endregion

    #region Characters Events

    public static Evt<Character> OnCharacterDeath;

    #endregion

    #region Tile Selector Events

    public static Evt<Character> OnCharacterHovered;
    public static Evt<Character> OnCharacterUnhovered;

    public static Evt<bool> OnSelectionTypeChanged; //todo bool is temporary, to replace with a more appropriate type
    public static Evt<SelectionCommand> OnCommandSent;

    #endregion

    #region Movement Events

    public static Evt<Tile> OnMovementStarted;

    #endregion

    #region Facing Direction Events

    public static Evt<Vector3> OnDirectionSelect;

    #endregion

    #region Turn Events

    public static Evt<TurnInformation> OnTurnChanged;

    #endregion

    #region Equipment Events

    public static Evt<EquipmentData> OnItemEquipped;
    public static Evt<EquipmentData> OnItemUnequipped;
    
    public static Evt<AbilityData> OnAbilityEquipped;
    public static Evt<AbilityData> OnAbilityUnequipped;

    #endregion

    #region Abilities Events

    public static Evt<IEnumerator> OnAbilityUsed;
    public static Evt OnAbilityFinished;

    #endregion

    #region Statistics Events

    public static Evt<StatisticValue> OnStatisticChanged;

    #endregion
}
