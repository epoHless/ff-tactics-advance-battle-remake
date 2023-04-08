using System;
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

        #region Unity Methods

        private void Awake()
        {
            CurrentTile = PathFinder.GetTileAtPosition(transform.position);
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            InputSystem.AddGridMovementListener(SelectTile);
            InputSystem.AddConfirmListener(ConfirmMovement);
        }

        private void OnDisable()
        {
            InputSystem.RemoveGridMovementListener(SelectTile);
            InputSystem.RemoveConfirmListener(ConfirmMovement);
        }

        #endregion

        #region Input Event Methods

        private void ConfirmMovement(InputAction.CallbackContext obj)
        {
            if(CurrentTile) EventManager.OnMovement?.Invoke(CurrentTile);
        }

        private void SelectTile(InputAction.CallbackContext obj)
        {
            var axis = InputSystem.GridAxis;
            var adjustedPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        
            var tile = PathFinder.GetTileAtPosition(adjustedPosition, new Vector3(axis.x, 0, axis.y));
            CurrentTile = tile ? tile : CurrentTile;

            if(CurrentTile) transform.position = CurrentTile.ArrivalTransform.position;
            
            IsCharacterOnTile();
        }

        public void IsCharacterOnTile()
        {
            if (CharacterOnTile(out Character character))
                EventManager.OnCharacterHovered?.Invoke(character);
            else
                EventManager.OnCharacterUnhovered?.Invoke();
        }

        /// <summary>
        /// Checks if a character is in the current tile
        /// </summary>
        /// <param name="_character"></param>
        /// <returns></returns>
        private bool CharacterOnTile(out Character _character)
        {
            var rayOrigin = transform.position + (Vector3.up * 2f);
            Ray ray = new Ray(rayOrigin, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                if (hit.transform.TryGetComponent(out Character character))
                {
                    _character = character;
                    return true;
                }
            }

            _character = null;
            return false;
        }

        #endregion

        #region Methods

        public void ToggleSelector(bool _toggle)
        {
            gameObject.SetActive(_toggle);
        }

        #endregion
    }
}

