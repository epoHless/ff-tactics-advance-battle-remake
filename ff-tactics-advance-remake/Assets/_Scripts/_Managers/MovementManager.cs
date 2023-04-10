using System;
using System.Collections;
using GridSystem;
using UnityEngine;

public class MovementManager : Singleton<MovementManager>
{
    [SerializeField] private TurnInformation characterTurn;

    
    #region Properties

    public bool IsMoving { get; private set; }
    
    public TurnInformation CharacterTurn
    {
        get => TurnManager.Instance.currentTurn;
        private set => characterTurn = value;
    }

    #endregion

    #region Unity Events

    protected override void OnEnable()
    {
        base.OnEnable();
        
        EventManager.OnMovement += CallMoveCharacter;
        EventManager.OnDirectionSelect += SelectDirection;
        EventManager.OnTurnChanged += SetCharacter;
    }

    private void OnDisable()
    {
        EventManager.OnMovement -= CallMoveCharacter;
        EventManager.OnDirectionSelect -= SelectDirection;
        EventManager.OnTurnChanged -= SetCharacter;
    }

    #endregion

    #region Methods

    public void ActivateTilesInRange()
    {
        foreach (var tile in PathFinder.GetTilesInRange(CharacterTurn.Character))
        {
            tile.SelectionBox.SetActive(true);
        }
    }
    
    public void DeactivateTilesInRange()
    {
        foreach (var tile in PathFinder.GetTilesInRange(CharacterTurn.Character))
        {
            tile.SelectionBox.SetActive(false);
        }
    }

    #endregion

    #region Movement Event Methods

    private void CallMoveCharacter(Tile _endTile)
    {
        if (!PathFinder.GetTilesInRange(CharacterTurn.Character).Contains(_endTile) || CharacterTurn.Character.Movement.OccupiedTile == _endTile) return;
        
        StartCoroutine(nameof(MoveCharacter), _endTile);
        DeactivateTilesInRange();
    }

    IEnumerator MoveCharacter(Tile _endTile)
    {
        var path = PathFinder.CalculatePath(CharacterTurn.Character, _endTile);

        IsMoving = true;
        
        foreach (var tile in path)
        {
            yield return CharacterTurn.Character.Movement.Move(tile);
        }
        
        EventManager.OnCharacterHovered?.Invoke(CharacterTurn.Character); //reselect character when the movement is done
        CharacterTurn.HasMoved = true;

        IsMoving = false;
    }

    #endregion
    
    #region Facing Direction Methods

    private void SelectDirection(Vector3 _direction)
    {
        var targetRot = CharacterTurn.Character.transform.position + _direction;
        targetRot.y = CharacterTurn.Character.transform.position.y;
        
        CharacterTurn.Character.transform.LookAt(targetRot);
    }

    #endregion

    #region Turn Methods

    private void SetCharacter(TurnInformation _turnInformation)
    {
        CharacterTurn = _turnInformation;
    }

    #endregion
}
