using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

[CreateAssetMenu(fileName = "InputReader", menuName = "Lab4Week5/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2> Attack = delegate { };

    PlayerInputActions inputActions;

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
    }

    void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Disable();
            inputActions.UI.Disable();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack.Invoke(Vector2.zero);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //noop
    }
}
