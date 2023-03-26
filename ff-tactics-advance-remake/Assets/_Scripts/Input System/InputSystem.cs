using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FinalFantasy
{
    public static class InputSystem
    {
        #region Properties

        private static PlayerInputActions Actions { get; set; }
        
        public static Vector2 GridAxis => Actions.Combat.GridMovement.ReadValue<Vector2>();
        private static bool IsInit { get; set; } = false;

        #endregion

        #region Constructor

        static InputSystem()
        {
            Init();
        }

        private static void Init()
        {
            if (!IsInit)
            {
                Actions = new PlayerInputActions();
                IsInit = true;
            }
        }

        #endregion

        #region Methods

        public static void EnableGridMovement()
        {
            Actions.Combat.GridMovement.Enable();
        }
        
        public static void DisableGridMovement()
        {
            Actions.Combat.GridMovement.Disable();
        }
        
        public static void EnableConfirm()
        {
            Actions.Combat.Confirm.Enable();
        }
        
        public static void DisableConfirm()
        {
            Actions.Combat.Confirm.Disable();
        }

        public static void AddGridMovementListener(Action<InputAction.CallbackContext> context)
        {
            Actions.Combat.GridMovement.started += context;
        }
        
        public static void RemoveGridMovementListener(Action<InputAction.CallbackContext> context)
        {
            Actions.Combat.GridMovement.started -= context;
        }
        
        public static void AddConfirmListener(Action<InputAction.CallbackContext> context)
        {
            Actions.Combat.Confirm.started += context;
        }
        
        public static void RemoveConfirmListener(Action<InputAction.CallbackContext> context)
        {
            Actions.Combat.Confirm.started -= context;
        }

        #endregion
    }
}