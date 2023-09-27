using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControlsInputSystem : MonoBehaviour
{
    public bool leftClickPerformed = false;
    public bool leftClickCanceled = false;


    public void InputLeftClick(InputAction.CallbackContext context) {
        Debug.Log("Left Click!" + context.phase);
        leftClickPerformed = context.phase == InputActionPhase.Performed;
        leftClickCanceled = context.phase == InputActionPhase.Canceled;
    }

    public void InputCursorMovement(InputAction.CallbackContext context) {
        Debug.Log("Mouse Delta:"+context.ReadValue<Vector2>());
    }
}
