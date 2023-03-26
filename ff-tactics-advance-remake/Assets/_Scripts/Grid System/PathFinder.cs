using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;

public class PathFinder
{
    public static List<Tile> GetPath(Character _character, Tile _endTile)
    {
        var OpenTiles = new List<Tile>();
        var ClosedTiles = new List<Tile>();
        
        OpenTiles.Add(_character.Movement.OccupiedTile);

        while (OpenTiles.Count > 0)
        {
            Tile currentTile = OpenTiles.OrderBy(x => x.F).First();
            
            OpenTiles.Remove(currentTile);
            ClosedTiles.Add(currentTile);

            if (currentTile == _endTile)
            {
                return GetFinishedList(_character.Movement.OccupiedTile, _endTile);
            }

            var neighborTiles = GetNeighbors(currentTile);

            foreach (var neighborTile in currentTile.Neighbors)
            {
                if (ClosedTiles.Contains(neighborTile.Tile) || neighborTile.HeightDifference > 1 || !neighborTile.Tile.CanTravel)
                {
                    continue;
                }

                neighborTile.Tile.G = GetManhattanDistance(_character.Movement.OccupiedTile, neighborTile.Tile);
                neighborTile.Tile.H = GetManhattanDistance(_endTile, neighborTile.Tile);

                neighborTile.Tile.PreviousTile = currentTile;
                
                if (!OpenTiles.Contains(neighborTile.Tile))
                {
                    OpenTiles.Add(neighborTile.Tile);
                }
            }
        }

        return new List<Tile>();
    }
    
    public static List<Tile> GetTilesInRange(Character _character)
    {
        var startingTile = _character.Movement.OccupiedTile;
        var inRangeTiles = new List<Tile>();
        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        var tilesForPreviousStep = new List<Tile>();
        tilesForPreviousStep.Add(startingTile);
        
        while (stepCount < _character.Movement.MovementData.Range)
        {
            var surroundingTiles = new List<Tile>();

            foreach (var tile in tilesForPreviousStep) //todo add height check
            {
                surroundingTiles.AddRange(GetNeighbors(tile));
            }

            inRangeTiles.AddRange(surroundingTiles);
            tilesForPreviousStep = surroundingTiles.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }
    
    public static Tile GetTileFromPosition(Vector3 _position)
    {
        Ray ray = new Ray(_position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, 1 << 6))
        {
            if (hit.transform.TryGetComponent<Tile>(out Tile tile))
            {
                return tile;
            }
        }

        return null;
    }
    
    public static Tile GetTileFromPosition(Vector3 _position, Vector3 _direction)
    {
        Ray ray = new Ray(_position, _direction);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, 1 << 6))
        {
            if (hit.transform.TryGetComponent<Tile>(out Tile tile))
            {
                return tile;
            }
        }

        return null;
    }

    private static List<Tile> GetFinishedList(Tile _start, Tile _end)
    {
        List<Tile> finishedList = new List<Tile>();

        Tile currentTile = _end;

        while (currentTile != _start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.PreviousTile;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private static int GetManhattanDistance(Tile _start, Tile _neighbor)
    {
        return Mathf.CeilToInt(Mathf.Abs(_start.transform.position.x - _neighbor.transform.position.x) +  Mathf.Abs(_start.transform.position.y - _neighbor.transform.position.y));
    }

    private static List<Tile> GetNeighbors(Tile currentTile)
    {
        List<Tile> neighbors = new List<Tile>();

        foreach (var neighbor in currentTile.Neighbors)
        {
            neighbors.Add(neighbor.Tile);
        }

        return neighbors;
    }
}
