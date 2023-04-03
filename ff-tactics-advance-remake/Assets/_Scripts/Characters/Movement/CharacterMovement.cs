using System;
using System.Collections;
using GridSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Properties

    [field: SerializeField] public Tile OccupiedTile { get; private set; }
    [field: SerializeField, SORender] public MovementData MovementData { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    #endregion

    #region Unity Methods

    private void Start() { OccupiedTile = PathFinder.GetTileAtPosition(transform.position); }

    #endregion

    #region Methods

    public IEnumerator Move(Tile _nextTile)
    {
        var isArrived = false;
        LeanTween.move(gameObject, _nextTile.ArrivalTransform.position, 1 / Speed).setOnComplete(() => isArrived = true);
        yield return new WaitUntil(() => isArrived);
        OccupiedTile = _nextTile;
    }

    #endregion
}
