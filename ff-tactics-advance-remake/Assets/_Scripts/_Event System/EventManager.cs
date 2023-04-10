using GridSystem;
using UnityEngine;

public static class EventManager
{
    public delegate void Evt();
    public delegate void Evt<T>(T item);

    #region Tile Selector Events

    public static Evt<Character> OnCharacterHovered;
    public static Evt<Character> OnCharacterUnhovered;

    #endregion

    #region Movement Events

    public static Evt<Tile> OnMovement;

    #endregion

    #region Facing Direction Events

    public static Evt<Vector3> OnDirectionSelect;

    #endregion

    #region Turn Events

    public static Evt<TurnInformation> OnTurnChanged;

    #endregion
}
