using Unity.VisualScripting;
using UnityEngine;

public class FishLocation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider colission)
    {
        if(colission.gameObject.tag == "FishZone1")
        {
            Debug.Log("Entered fishing zone 1");
        }

        if (colission.gameObject.tag == "FishZone2")
        {
            Debug.Log("Entered fishing zone 2");
        }

        if (colission.gameObject.tag == "FishZone3")
        {
            Debug.Log("Entered fishing zone 3");
        }
    }
}
