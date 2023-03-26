using System;
using System.Collections;
using GridSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [field: SerializeField] public Tile OccupiedTile { get; private set; }
    [field: SerializeField, SORender] public MovementData MovementData { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    private bool IsArrived { get; set; }

    private void Start()
    {
        OccupiedTile = PathFinder.GetTileFromPosition(transform.position);

    }

    public IEnumerator Move(Tile _nextTile)
    {
        IsArrived = false;
        LeanTween.move(gameObject, _nextTile.ArrivalTransform.position, Speed).setOnComplete(() => IsArrived = true);
        yield return new WaitUntil(() => IsArrived);
        OccupiedTile = _nextTile;
    }
}
