using GridSystem;

public class MovementCommand : SelectionCommand
{
    public override void Execute(TileSelector _tileSelector)
    {
        if(_tileSelector.CurrentTile && !_tileSelector.IsCharacterOnTile()) EventManager.OnMovementStarted?.Invoke(_tileSelector.CurrentTile);
    }
}