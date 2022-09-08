using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MemezawyDev.Player.Input
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputManager : MonoBehaviour
    {
        public enum InputSate
        {
            Ended,
            Preformed,
            Started
        }
        public static class ControleSchemes
        {
            public static string[] Controller = { "JoyStick" , "Gamepad"};
            public static string KeyboadAndMouse = "KeyboadAndMouse";
        }

        private PlayerControlls _playerControlls;
        private PlayerInput _playerInput;

        private InputAction _movementInput;
        private InputAction _lookInput;

        [HideInInspector]
        public RefrenceVector3 MovementVector { get; private set; }
        public Vector2 Look { get; private set; }
        public InputSate Jump { get; private set; }
        public InputSate Dash { get; private set; }
        public InputSate Fire { get; private set; }
        public InputSate Grapple { get; private set; }
        public InputSate CancelGrapple { get; private set; }
        public InputSate Pause { get; private set; }

        private string CurrentControleScheme => _playerInput.currentControlScheme;
        public bool IsUsingController => ControleSchemes.Controller.Contains<string>(CurrentControleScheme);
        public bool IsUsingKeyboardAndMouse => ControleSchemes.KeyboadAndMouse == CurrentControleScheme;


        private void OnEnable()
        {
            _playerControlls.Enable();
        }

        private void Awake()
        {

            _playerControlls = new PlayerControlls();
            MovementVector = new RefrenceVector3();
            _movementInput = _playerControlls.Player.Move;
            _lookInput = _playerControlls.Player.Look;
            _playerInput = GetComponent<PlayerInput>();
            
        }
        private void Update()
        {
            MovementVector.x = _movementInput.ReadValue<Vector2>().x;
            MovementVector.y = _movementInput.ReadValue<Vector2>().y;
            Look = _lookInput.ReadValue<Vector2>();
        }

        #region Input Handlers

        public void HandleJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Jump = InputSate.Started;
            }
            else if (context.performed)
            {
                Jump = InputSate.Preformed;
            }
            else if (context.canceled)
            {
                Jump = InputSate.Ended;
            }
        }
        public void HandleFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Fire = InputSate.Started;
            }
            else if (context.performed)
            {
                Fire = InputSate.Preformed;
            }
            else if (context.canceled)
            {
                Fire = InputSate.Ended;
            }
        }
        public void HandleDash(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Dash = InputSate.Started;
            }
            else if (context.performed)
            {
                Dash = InputSate.Preformed;
            }
            else if (context.canceled)
            {
                Dash = InputSate.Ended;
            }
        }
        public void HandleGrapple(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Grapple = InputSate.Started;
            }
            else if (context.performed)
            {
                Grapple = InputSate.Preformed;
            }
            else if (context.canceled)
            {
                Grapple = InputSate.Ended;
            }
        }
        public void HandleCancelGrapple(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                CancelGrapple = InputSate.Started;
            }
            else if (context.performed)
            {
                CancelGrapple = InputSate.Preformed;
            }
            else if (context.canceled)
            {
                CancelGrapple = InputSate.Ended;
            }
        }

        public void HandlePause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Pause = InputSate.Started;
            }
            else if (context.performed)
            {
                Pause = InputSate.Preformed;
            }
            else if (context.canceled)
            {
                Pause = InputSate.Ended;
            }
        }

        #endregion


        private void OnDisable()
        {
            _playerControlls.Disable();
        }
    }
}


