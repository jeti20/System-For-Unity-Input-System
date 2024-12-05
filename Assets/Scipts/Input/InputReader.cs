using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


namespace Pairdot
{
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
    {
        private GameInput gameInput;

        //Gameplay and UI are the names of created Maps in Inputsystem
        private void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new GameInput();

                gameInput.Gameplay.SetCallbacks(instance:this);
                gameInput.UI.SetCallbacks(instance:this);

                //If game is running, gameplay map is set enable by default
                gameInput.Gameplay.Enable();
            }

        }

        //Enabling mapping - make sure only one function is enalbed at the same time
        public void SetGemplay()
        {
            gameInput.Gameplay.Enable();
            gameInput.UI.Disable();
        }

        //Enabling mapping - make sure only one function is enalbed at the same time
        public void SetUI()
        {
            gameInput.Gameplay.Disable();
            gameInput.UI.Enable();
        }

        //move
        public event Action<Vector2> MoveEvent; //MoveEvent will always know what the input is

        //jump
        public event Action JumpEvent;
        public event Action JumpCancelledEvent;

        public event Action PauseEvent;
        public event Action ResumeEvent;


        public void OnMove(InputAction.CallbackContext context)
        {
            //MoveEvent will always know what the input is
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
            Debug.Log($"Phase: {context.phase}, Value: {context.ReadValue<Vector2>()}");
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase ==InputActionPhase.Performed)
            {
                JumpEvent?.Invoke();
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                JumpCancelledEvent?.Invoke();
            }
        }       

        public void OnPosue(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUI(); //enabling the UI map if PouseEvent is Invoked
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            ResumeEvent?.Invoke();
            SetGemplay(); //enabling the Gameplay map if ResumeEvent
                          //is Invoked
        }
    }

}

