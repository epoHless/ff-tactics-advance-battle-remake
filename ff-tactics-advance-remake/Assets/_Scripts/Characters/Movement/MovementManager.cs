using System;
using System.Collections;
using GridSystem;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : Singleton<MovementManager>
{
    [field: SerializeField] public Character Character { get; private set; }

    public static UnityEvent<Tile> OnMovement = new UnityEvent<Tile>();

    protected override void OnEnable()
    {
        base.OnEnable();
        
        OnMovement.AddListener(CallMoveCharacter);
    }

    private void Start()
    {
        ActivateTilesInRange(); //todo remove when state machine is implemented
    }

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

    private void CallMoveCharacter(Tile _endTile)
    {
        if (!PathFinder.GetTilesInRange(Character).Contains(_endTile)) return;
        
        StartCoroutine(nameof(MoveCharacter), _endTile);
        DeactivateTilesInRange();
    }

    IEnumerator MoveCharacter(Tile _endTile)
    {
        var path = PathFinder.GetPath(Character, _endTile);

        foreach (var tile in path)
        {
            yield return Character.Movement.Move(tile);
        }
        
        ActivateTilesInRange();
    }
}
