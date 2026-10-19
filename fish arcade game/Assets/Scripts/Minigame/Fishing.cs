
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    [SerializeField] Slider fishSlider;
    [SerializeField] float reelSpeed = 50;
    [SerializeField] private Transform handle;
    
    float fishVector;

    public void OnFish(InputAction.CallbackContext context)
    {
        fishVector = context.ReadValue<float>();
    }

    private void Update()
    {
        fishSlider.value += fishVector * reelSpeed * Time.deltaTime;
        handle.transform.rotation = Quaternion.Euler(handle.transform.rotation.eulerAngles.x, handle.transform.rotation.eulerAngles.y, handle.transform.rotation.eulerAngles.z + (fishVector * 300 * Time.deltaTime));
    }
}
