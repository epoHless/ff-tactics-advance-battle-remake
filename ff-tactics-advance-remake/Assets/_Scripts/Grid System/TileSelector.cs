using System;
using GridSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

public class TileSelector : MonoBehaviour
{
    [field: SerializeField] public Tile currentTile { get; private set; }

    private void Awake()
    {
        currentTile = PathFinder.GetTileFromPosition(transform.position);
    }

    private void OnEnable()
    {
        InputSystem.EnableGridMovement();
        InputSystem.EnableConfirm();
        
        InputSystem.AddGridMovementListener(CallMoveSelector);
        InputSystem.AddConfirmListener(CallConfirmMovement);
    }

    private void CallConfirmMovement(InputAction.CallbackContext obj)
    {
        MovementManager.OnMovement?.Invoke(currentTile);
    }

    private void CallMoveSelector(InputAction.CallbackContext obj)
    {
        var axis = obj.ReadValue<Vector2>();

        var adjustedPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        currentTile = PathFinder.GetTileFromPosition(adjustedPosition, new Vector3(axis.x, 0, axis.y));

        if(currentTile) transform.position = currentTile.ArrivalTransform.position;
    }
}
