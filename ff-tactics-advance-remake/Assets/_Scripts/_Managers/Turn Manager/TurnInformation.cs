using GridSystem;
using UnityEngine;

[System.Serializable]
public class TurnInformation
{
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public Tile StartingTile { get; private set; }
    [field: SerializeField] public bool HasMoved { get; set; }
    [field: SerializeField] public bool HasUsedAbilities { get; set; }

    public TurnInformation(Character _character)
    {
        Character = _character;
        
        StartingTile = Character.Movement.OccupiedTile != null ? 
            Character.Movement.OccupiedTile : PathFinder.GetTileAtPosition(Character.transform.position);

        HasMoved = false;
        HasUsedAbilities = false;
    }
}
