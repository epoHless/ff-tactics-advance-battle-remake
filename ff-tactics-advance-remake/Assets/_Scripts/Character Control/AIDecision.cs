using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "AIDecision_", menuName = "Game/Controls/New AI Decision", order = 0)]
public class AIDecision : CharacterDecision
{
    public override void MovementDecision()
    {
        MovementManager.Instance.ActivateTilesInRange(ControlledCharacter.Movement.MovementData.Range);
        
        var tiles = PathFinder.GetTilesInRange(ControlledCharacter, ControlledCharacter.Movement.MovementData.Range);
        var filteredTiles = tiles.Where(tile => tile != ControlledCharacter.Movement.OccupiedTile).ToList();
        
        var characters = TurnManager.Instance.Characters;

        var closestCharacter = characters.OrderBy(character => (character.transform.position - ControlledCharacter.transform.position).magnitude).ToList();
        var filteredClosestCharacters = closestCharacter.Where(character => character != ControlledCharacter).ToList();
        
        if (ControlledCharacter.BattleStatistics.CurrentHP < ControlledCharacter.BattleStatistics.HP * 0.25f) //if has less than a quarter of HP -> flee to the furthest tile from closest character
        {
            var chosenTile = filteredTiles.OrderBy(tile => Vector3.Distance(tile.transform.position, filteredClosestCharacters[0].transform.position)).ToList();

            EventManager.OnMovementStarted?.Invoke(chosenTile[0]);
            TurnManager.Instance.currentTurn.HasMoved = true;
        }
        else if (ControlledCharacter.BattleStatistics.CurrentHP > ControlledCharacter.BattleStatistics.HP * 0.25f)
        {
            var chosenTile = filteredTiles.OrderBy(tile => (tile.transform.position - filteredClosestCharacters[0].transform.position).magnitude).ToList();
            
            EventManager.OnMovementStarted?.Invoke(chosenTile[0]);
            
            Debug.Log($"{ControlledCharacter.Data.Name} Has Moved to {chosenTile[0].transform.position}!");
            TurnManager.Instance.currentTurn.HasMoved = true;
        }
        
        MovementManager.Instance.DeactivateTilesInRange(ControlledCharacter.Movement.MovementData.Range);
        
        FaceDecision();
    }

    protected override void ActionDecision()
    {
        foreach (var ability in ControlledCharacter.EquippedAbilities)
        {
            var tiles = PathFinder.GetTilesInRange(ControlledCharacter, ability.AbilityRange);

            foreach (var tile in tiles)
            {
                if (PathFinder.CharacterOnTile(tile, out Character _character) && _character != ControlledCharacter)
                { 
                    EventManager.OnAbilityUsed?.Invoke(ability.Execute(ControlledCharacter, _character));
                    Debug.Log($"{ControlledCharacter.Data.Name} Has used {ability.Name}!");
                    return;
                }
            }
        }

        if (ControlledCharacter.HasNeighbors(out List<Character> _neighbors))
        {
            EventManager.OnAbilityUsed?.Invoke(ControlledCharacter.CurrentJob.BaseAttack.Execute(ControlledCharacter, _neighbors[Random.Range(0, _neighbors.Count)]));
            Debug.Log($"{ControlledCharacter.Data.Name} Has used {ControlledCharacter.CurrentJob.BaseAttack.Name}!");
        }

        TurnManager.Instance.currentTurn.HasUsedAbilities = true;
        
        FaceDecision();
    }
}
