using UnityEngine;
using UnityEngine.InputSystem;

public class GameStates : MonoBehaviour
{
    [SerializeField] int playerID;

    public void StartFish(InputAction.CallbackContext context)
    {
        SwitchFishState(playerID);
    }

    public static void SwitchFishState(int player)
    {
        BoatMovement.toggleMovement?.Invoke(false, player);
        Catch.StartFishing?.Invoke(player);
    }

    public static void SwitchBoatState(int player)
    {
        BoatMovement.toggleMovement?.Invoke(true, player);
    }
}
