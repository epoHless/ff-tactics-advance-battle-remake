using GridSystem;
using UnityEngine;

[System.Serializable]
public class NeighborTile
{
    #region Properties

    [field: SerializeField] public Tile Tile;
    [field: SerializeField] public Vector3Int Direction;
    [field: SerializeField] public int HeightDifference;

    #endregion

    #region Constructors

    public NeighborTile(Tile _tile, Vector3Int _direction,int _height)
    {
        Tile = _tile;
        Direction = _direction;
        HeightDifference = Mathf.Abs(_tile.Height - _height);
    }

    #endregion

    #region Methods

    #endregion

}
