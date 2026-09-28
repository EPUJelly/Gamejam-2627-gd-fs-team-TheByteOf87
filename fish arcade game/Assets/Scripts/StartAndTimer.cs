using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class StartAndTimer : MonoBehaviour
{
    [SerializeField] private int gameTime = 210;
    private int remainingTime;
    private bool isRunning = false;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject startText;
    [SerializeField] private Image timerImage;
    [SerializeField] private List<Sprite> timerSprs;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private TMP_Text scoreP1;
    [SerializeField] private TMP_Text scoreP2;
    [SerializeField] private TMP_Text winner;
    private bool canStart = true;
    [SerializeField] private Transform boatStart1;
    [SerializeField] private Transform boatStart2;
    [SerializeField] private Transform boat1;
    [SerializeField] private Transform boat2;
    [SerializeField] private AudioSource startSfx;
    [SerializeField] private AudioSource endSfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startCanvas.SetActive(true);
        remainingTime = gameTime;
        EnableText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning == false)
        {
            BoatP1Movement.toggleMovementP1(false);
            BoatP2Movement.toggleMovementP2(false);
            if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X) && canStart)
            {
                StartGame();
            }
            
        }
        timerImage.sprite = timerSprs[210 - remainingTime];
    }

    public void StartGame()
    {
        endCanvas.SetActive(false);
        Invoke("EndGame", gameTime);
        isRunning = true;
        BoatP1Movement.toggleMovementP1(true);
        BoatP2Movement.toggleMovementP2(true);
        Invoke("DeductTime", 0);
        startCanvas.SetActive(false);
        canStart = false;
        boat1.position = boatStart1.position;
        boat1.rotation = boatStart1.rotation;
        boat2.position = boatStart2.position;
        boat2.rotation = boatStart2.rotation;
        startSfx.Play();
    }

    private void DeductTime()
    {
        remainingTime -= 1;
        if (isRunning == true)
        {
            Invoke("DeductTime", 1);
        }
    }

    public void EndGame()
    {
        isRunning = false;
        BoatP1Movement.toggleMovementP1(false);
        BoatP2Movement.toggleMovementP2(false);
        endCanvas.SetActive(true);
        scoreP1.text = this.GetComponent<Inventory>().scoreP1.ToString();
        scoreP2.text = this.GetComponent<Inventory>().scoreP2.ToString();
        if(this.GetComponent<Inventory>().scoreP1 > this.GetComponent<Inventory>().scoreP2)
        {
            winner.text = "P1 WINS!";
        }
        if (this.GetComponent<Inventory>().scoreP1 < this.GetComponent<Inventory>().scoreP2)
        {
            winner.text = "P2 WINS!";
        }
        if (this.GetComponent<Inventory>().scoreP1 == this.GetComponent<Inventory>().scoreP2)
        {
            winner.text = "TIE!";
        }
        Invoke("ResetEnd", 10);
        endSfx.Play();
    }

    private void ResetEnd()
    {
        endCanvas.SetActive(false);
        startCanvas.SetActive(true);
        canStart = true;
    }

    private void EnableText()
    {
        startText.SetActive(true);
        Invoke("DisableText", 1);
    }

    private void DisableText()
    {
        startText.SetActive(false);
        Invoke("EnableText", 1);
    }

    private float Remap(float val, float low1, float high1, float low2, float high2)
    {
        return low2 + (val - low1) * (high2 - low2) / (high1 - low1);
    }
}
