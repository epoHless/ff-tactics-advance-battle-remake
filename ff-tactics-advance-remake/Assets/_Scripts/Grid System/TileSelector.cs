using System;
using GridSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

namespace GridSystem
{
    public class TileSelector : MonoBehaviour
    {
        private SelectionCommand selectionCommand;
        [SerializeField] private Transform meshTransform;

        #region Properties

        [field: SerializeField] public Tile CurrentTile { get; private set; }
        
        public static Character CurrentCharacter { get; private set; }

        public SelectionCommand SelectionCommand
        {
            get => selectionCommand;
            set
            {
                selectionCommand = value;
                EventManager.OnSelectionTypeChanged?.Invoke(selectionCommand is MovementCommand);
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            CurrentTile = PathFinder.GetTileAtPosition(transform.position);
        }

        private void Start()
        {
            gameObject.SetActive(false);
            
            meshTransform.LeanScale(Vector3.one * 0.85f, 1f).setEaseInOutBack().setLoopPingPong();
        }

        private void OnEnable()
        {
            InputSystem.AddGridMovementListener(SelectTile);
            InputSystem.AddConfirmListener(ConfirmSelection);
            
            EventManager.OnCommandSent += SetCommand;
        }

        private void OnDisable()
        {
            InputSystem.RemoveGridMovementListener(SelectTile);
            InputSystem.RemoveConfirmListener(ConfirmSelection);
            
            EventManager.OnCommandSent -= SetCommand;
        }

        #endregion

        #region Input Event Methods

        private void ConfirmSelection(InputAction.CallbackContext obj)
        {
            if(CurrentTile && CurrentTile.CanTravel) SelectionCommand.Execute(this);
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

        public bool IsCharacterOnTile()
        {
            if (CharacterOnTile(out Character character))
            {
                EventManager.OnCharacterHovered?.Invoke(character);
                CurrentCharacter = character;
                return true;
            }
            else
            {
                EventManager.OnCharacterUnhovered?.Invoke(character);
                CurrentCharacter = null;
                return false;
            }
        }

        /// <summary>
        /// Checks if a character is in the current tile
        /// </summary>
        /// <param name="_character"></param>
        /// <returns></returns>
        public bool CharacterOnTile(out Character _character)
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

        #region Command Events

        private void SetCommand(SelectionCommand _command)
        {
            SelectionCommand = _command;
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

