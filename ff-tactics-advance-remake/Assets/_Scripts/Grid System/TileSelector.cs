using GridSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

namespace GridSystem
{
    public class TileSelector : MonoBehaviour
    {
        #region Properties

        [field: SerializeField] public Tile CurrentTile { get; private set; }

        #endregion

        #region Unity Method

        private void Awake()
        {
            CurrentTile = PathFinder.GetTileAtPosition(transform.position);
        }

        private void OnEnable()
        {
            InputSystem.EnableGridMovement();
            InputSystem.EnableConfirm();
        
            InputSystem.AddGridMovementListener(SelectTile);
            InputSystem.AddConfirmListener(ConfirmMovement);
        }

        private void OnDisable()
        {
            InputSystem.DisableGridMovement();
            InputSystem.DisableConfirm();
        
            InputSystem.RemoveGridMovementListener(SelectTile);
            InputSystem.RemoveConfirmListener(ConfirmMovement);
        }

        #endregion

        #region Input Event Methods

        private void ConfirmMovement(InputAction.CallbackContext obj)
        {
            MovementManager.OnMovement?.Invoke(CurrentTile);
        }

        private void SelectTile(InputAction.CallbackContext obj)
        {
            var axis = InputSystem.GridAxis;
            var adjustedPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        
            var tile = PathFinder.GetTileAtPosition(adjustedPosition, new Vector3(axis.x, 0, axis.y));
            CurrentTile = tile ? tile : CurrentTile;

            if(CurrentTile) transform.position = CurrentTile.ArrivalTransform.position;
        }

        #endregion
    }
}

