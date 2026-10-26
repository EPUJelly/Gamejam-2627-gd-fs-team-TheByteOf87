using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameObject.Find("Directional Light").GetComponent<DayCycle>().timeOfDay == "night")
        {
            this.GetComponentInChildren<Light>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
