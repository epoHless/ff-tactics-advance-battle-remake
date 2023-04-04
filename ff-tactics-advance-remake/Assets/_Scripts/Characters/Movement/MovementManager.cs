using System;
using System.Collections;
using GridSystem;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : Singleton<MovementManager>
{
    #region Properties

    [field: SerializeField] public Character Character { get; private set; }

    #endregion

    #region Events


    #endregion

    #region Unity Events

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnMovement += CallMoveCharacter;
    }

    private void OnDisable()
    {
        EventManager.OnMovement -= CallMoveCharacter;
    }

    private void Start()
    {
        ActivateTilesInRange(); //todo remove when state machine is implemented
    }

    #endregion

    #region Methods

    private void ActivateTilesInRange() //todo call this on turn start
    {
        foreach (var tile in PathFinder.GetTilesInRange(Character))
        {
            tile.SelectionBox.SetActive(true);
        }
    }
    
    private void DeactivateTilesInRange()
    {
        foreach (var tile in PathFinder.GetTilesInRange(Character))
        {
            tile.SelectionBox.SetActive(false);
        }
    }

    #endregion

    #region Movement Event Methods

    private void CallMoveCharacter(Tile _endTile)
    {
        if (!PathFinder.GetTilesInRange(Character).Contains(_endTile) || Character.Movement.OccupiedTile == _endTile) return;
        
        StartCoroutine(nameof(MoveCharacter), _endTile);
        DeactivateTilesInRange();
    }

    IEnumerator MoveCharacter(Tile _endTile)
    {
        var path = PathFinder.CalculatePath(Character, _endTile);

        foreach (var tile in path)
        {
            yield return Character.Movement.Move(tile);
        }
        
        EventManager.OnCharacterHovered?.Invoke(Character);
        ActivateTilesInRange();
    }

    #endregion
}
