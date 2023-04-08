﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FinalFantasy
{
    public static class InputSystem
    {
        #region Properties

        private static PlayerInputActions Actions { get; set; }
        
        public static Vector2 GridAxis => Actions.Movement.GridMovement.ReadValue<Vector2>();

        public static bool WasConfirmPressed => Actions.Menu.Confirm.WasPressedThisFrame();
        public static bool WasBackPressed => Actions.Menu.Back.WasPressedThisFrame();
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
                Actions.Enable();
                IsInit = true;
            }
        }

        #endregion

        #region Methods

        #region Inputs

        public static void EnableInput()
        {
            Actions.Enable();
        }
        
        public static void DisableInput()
        {
            Actions.Disable();
        }

        #endregion
        
        #region Grid Movement

        public static void EnableMovement()
        {
            Actions.Movement.Enable();
        }
        
        public static void DisableMovement()
        {
            Actions.Movement.Disable();
        }
        
        public static void EnableGridMovement()
        {
            Actions.Movement.GridMovement.Enable();
        }
        
        public static void DisableGridMovement()
        {
            Actions.Movement.GridMovement.Disable();
        }
        
        public static void AddGridMovementListener(Action<InputAction.CallbackContext> context, EInputType _inputType = EInputType.STARTED)
        {
            switch (_inputType)
            {
                case EInputType.STARTED:
                    Actions.Movement.GridMovement.started += context;
                    break;
                case EInputType.PERFORMED:
                    Actions.Movement.GridMovement.performed += context;
                    break;
                case EInputType.CANCELLED:
                    Actions.Movement.GridMovement.canceled += context;
                    break;
            }
        }
        
        public static void RemoveGridMovementListener(Action<InputAction.CallbackContext> context, EInputType _inputType = EInputType.STARTED)
        {
            switch (_inputType)
            {
                case EInputType.STARTED:
                    Actions.Movement.GridMovement.started -= context;
                    break;
                case EInputType.PERFORMED:
                    Actions.Movement.GridMovement.performed -= context;
                    break;
                case EInputType.CANCELLED:
                    Actions.Movement.GridMovement.canceled -= context;
                    break;
            }
        }

        #endregion

        #region Confirm Tile Selection

        public static void EnableConfirm()
        {
            Actions.Menu.Confirm.Enable();
        }
        
        public static void DisableConfirm()
        {
            Actions.Menu.Confirm.Disable();
        }

        public static void AddConfirmListener(Action<InputAction.CallbackContext> context, EInputType _inputType = EInputType.STARTED)
        {
            switch (_inputType)
            {
                case EInputType.STARTED:
                    Actions.Menu.Confirm.started += context;
                    break;
                case EInputType.PERFORMED:
                    Actions.Menu.Confirm.performed += context;
                    break;
                case EInputType.CANCELLED:
                    Actions.Menu.Confirm.canceled += context;
                    break;
            }
        }
        
        public static void RemoveConfirmListener(Action<InputAction.CallbackContext> context, EInputType _inputType = EInputType.STARTED)
        {
            switch (_inputType)
            {
                case EInputType.STARTED:
                    Actions.Menu.Confirm.started -= context;
                    break;
                case EInputType.PERFORMED:
                    Actions.Menu.Confirm.performed -= context;
                    break;
                case EInputType.CANCELLED:
                    Actions.Menu.Confirm.canceled -= context;
                    break;
            }
        }

        #endregion

        #region Facing Direction

        public static void EnableFacingDirection()
        {
            Actions.FaceDirection.Enable();
        }
        
        public static void DisableFacingDirection()
        {
            Actions.FaceDirection.Disable();
        }

        #endregion
        
        #endregion
    }
}