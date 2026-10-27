
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] GameObject inputPrefab;
    [SerializeField] PlayerInput[] playerInputs;

    private void Start()
    {
        AssignControllers();
    }
        
    public void AssignControllers()
    {
        switch (Gamepad.all.Count)
        {
            case 0:
                playerInputs[0].SwitchCurrentControlScheme("WASD", Keyboard.current);
                playerInputs[1].SwitchCurrentControlScheme("Arrows", Keyboard.current);
                break;
            case 1:
                playerInputs[0].SwitchCurrentControlScheme("WASD", Keyboard.current);
                playerInputs[1].SwitchCurrentControlScheme("Gamepad", Gamepad.current);
                break;
            default:
                playerInputs[0].SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
                playerInputs[1].SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
                break;
        }
    }
}
