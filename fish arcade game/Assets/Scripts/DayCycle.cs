using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private string timeOfDay;
    [SerializeField] private float noonHeight;
    [SerializeField] private float eveningHeight;
    [SerializeField] private float morningHeight;
    [SerializeField] private float nightHeight;
    [SerializeField] private bool custom = false;
    [SerializeField] private float customHeight;
    [SerializeField] private ParticleSystem stars;
    [SerializeField] private Color nightColor;
    [SerializeField] private Color dayColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetTOD(timeOfDay,custom, customHeight);
    }

    void SetTOD(string tod, bool isCustom, float customH)
    {
        if(isCustom == false)
        {
            if (tod == "morning")
            {
                GetComponent<Light>().color = dayColor;
                this.transform.rotation = Quaternion.Euler(morningHeight, 0, 0);
            }
            else if (tod == "evening")
            {
                GetComponent<Light>().color = dayColor;
                this.transform.rotation = Quaternion.Euler(eveningHeight, 0, 0);
            }
            else if (tod == "noon")
            {
                GetComponent<Light>().color = dayColor;
                this.transform.rotation = Quaternion.Euler(noonHeight, 0, 0);
            }
            else if (tod == "night")
            {
                GetComponent<Light>().color = nightColor;
                this.transform.rotation = Quaternion.Euler(nightHeight, 0, 0);
            }
            else
            {
                Debug.LogError("invalid tod string, use morning, evening, noon, or night");
            }
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(customH, 0, 0);
        }

        if(this.transform.rotation.eulerAngles.x < 0 || this.transform.rotation.eulerAngles.x > 180)
        {
            stars.gameObject.SetActive(true);
            stars.Play();
        }
        else
        {
            stars.Stop();
            stars.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
