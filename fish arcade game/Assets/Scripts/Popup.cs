using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static UnityAction<bool> togglePopup;
    public bool isEnabled = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        togglePopup += TogglePop;
        this.GetComponent<Image>().enabled = isEnabled;
        this.GetComponentInChildren<TMP_Text>().enabled = isEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F1))
        //{
        //    togglePopup.Invoke(true);
        //}
    }

    private void TogglePop(bool val)
    {
        isEnabled = val;
        this.GetComponent<Image>().enabled = isEnabled;
        this.GetComponentInChildren<TMP_Text>().enabled = isEnabled;
    }
}
