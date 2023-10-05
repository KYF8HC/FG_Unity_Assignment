using System;
using UnityEngine.InputSystem;

namespace KarioMart.Core
{
   public class InputHandler
   {
      private PlayerInputActions playerInputActions;

      public event Action OnPause;

      public void Initialize()
      {
         playerInputActions = new PlayerInputActions();
         playerInputActions.Enable();
         playerInputActions.Player.Pause.performed += Pause_performed;
      }

      private void Pause_performed(InputAction.CallbackContext obj)
      {
         OnPause?.Invoke();
      }

      public void Dispose()
      {
         playerInputActions.Disable();
         playerInputActions.Player.Pause.performed -= Pause_performed;
      }

      public PlayerInputActions GetPlayerInputActions()
      {
         return playerInputActions;
      }
   }
}