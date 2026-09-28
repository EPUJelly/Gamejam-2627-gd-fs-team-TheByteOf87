using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(checkDevices());
    }

    IEnumerator checkDevices()
    {
        while (true)
        {
            foreach (var device in InputSystem.devices)
            {
                Debug.Log(device);
            }

            yield return new WaitForSeconds(5);
        }
    }
}
