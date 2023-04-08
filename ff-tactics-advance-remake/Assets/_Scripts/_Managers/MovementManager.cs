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
        EventManager.OnDirectionSelect += SelectDirection;
    }

    private void OnDisable()
    {
        EventManager.OnMovement -= CallMoveCharacter;
        EventManager.OnDirectionSelect -= SelectDirection;
    }

    #endregion

    #region Methods

    public void ActivateTilesInRange()
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
        GameManager.Instance.ChangeState(GameManager.Instance.facingDirectionState);
    }
    
    private void SelectDirection(Vector3 _direction)
    {
        var targetRot = Character.transform.position + _direction;
        targetRot.y = Character.transform.position.y;
        
        Character.transform.LookAt(targetRot);
    }

    #endregion
}
