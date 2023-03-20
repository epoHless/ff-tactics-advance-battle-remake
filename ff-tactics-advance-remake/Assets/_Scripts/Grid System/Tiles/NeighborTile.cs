using GridSystem;
using UnityEngine;

[System.Serializable]
public class NeighborTile
{
    #region Properties

    [field: SerializeField] public Tile Tile;
    [field: SerializeField] public Vector3Int Direction;
    [field: SerializeField] public float Cost;

    #endregion

    #region Constructors

    public NeighborTile(Tile tile, Vector3Int direction, float cost)
    {
        Tile = tile;
        Direction = direction;
        Cost = cost;
    }

    #endregion

    #region Methods

    #endregion

}
