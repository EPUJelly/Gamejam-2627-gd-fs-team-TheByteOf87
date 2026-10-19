
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Catch : MonoBehaviour
{
    public static UnityAction<int> StartFishing;
    [SerializeField] Slider fishingSlider;
    [SerializeField] Slider targetSlider;
    [SerializeField] RawImage targetImage;
    [SerializeField] Texture2D[] targetImages = new Texture2D[5];
    [SerializeField] List<Vector2> targetSpeeds;
    [SerializeField] Vector2 targetSwitchTiming = new Vector2(0.1f, 1f);
    [SerializeField] int target;
    [SerializeField] int player;
    [SerializeField] float fishHealth = 3f;
    [SerializeField] private Image FishIcon;
    [SerializeField] private TextMeshProUGUI fishStats;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] private RectTransform handle;
    [SerializeField] private GameObject rod;
    private bool isFishingBool = false;
    private float handleSize;
    private float handleSizeTarg;
    float[] catchSizes = {25, 20, 16, 12, 8};
    float moveSpeed;
    bool stopFishing = true;
    public List<Fish> fishList = new List<Fish>();
    private Fish chosenFish;
    [SerializeField] private Transform fishTip;
    [SerializeField] private Transform fishTipEnd;
    [SerializeField] private Transform fishTipStart;
    private float fishTipLerp = 0;
    [SerializeField] private ParticleSystem catchFx;

    private void Awake()
    {
        StartFishing += FishingFish;
    }

    private void Start()
    {
        targetSlider.value = Random.Range(targetSlider.minValue + catchSizes[target] / 2, targetSlider.maxValue - catchSizes[target] / 2);
        
        targetImage.texture = targetImages[target];

        StartCoroutine(MoveRandomizer());

        
    }

    private void FixedUpdate()
    {
        handleSize = Mathf.Lerp(handleSize, handleSizeTarg, 0.3f);
    }

    private void Update()
    {
        fishTip.transform.position = Vector3.Lerp(fishTipStart.position, fishTipEnd.position, fishTipLerp);
        if (isFishingBool)
        {
            fishTipLerp = Mathf.Lerp(fishTipLerp, 1, 0.1f);
        }
        else
        {
            fishTipLerp = Mathf.Lerp(fishTipLerp, 0, 0.1f);
        }
        if (stopFishing) return;

        // Start catching fish if hook within target bounds
        if (fishingSlider.value < targetSlider.value + catchSizes[target] / 2 &&
            fishingSlider.value > targetSlider.value - catchSizes[target] / 2)
        {
            handleSizeTarg = 1.2f;
            fishHealth -= Time.deltaTime;
            
        }
        else
        {
            
            handleSizeTarg = 1f;
        }

        if (fishHealth <= 0) CatchFish();

        // Goofy code to keep the target image within bounds
        if (targetSlider.value + moveSpeed * Time.deltaTime > targetSlider.maxValue - catchSizes[target] / 2)
            return;
        else if (targetSlider.value + moveSpeed * Time.deltaTime < targetSlider.minValue + catchSizes[target] / 2)
            return;

        targetSlider.value += moveSpeed * Time.deltaTime;
        if(moveSpeed > 0)
        {
            handle.localScale = new Vector3(handleSize, 1, 1);
        }
        else if (moveSpeed < 0)
        {
            handle.localScale = new Vector3(handleSize, -1, 1);
        }

    }

    private void FishingFish(int playerNumber)
    {
        if (playerNumber != player) return;
        chosenFish = GetRandomFish();
        target = chosenFish.fishLength - 1;
        targetImage.texture = targetImages[chosenFish.fishLength - 1];
        playerInput.SwitchCurrentActionMap("Fishing");
        fishingSlider.gameObject.SetActive(true);
        targetSlider.gameObject.SetActive(true);
        fishHealth = 3f;
        rod.GetComponent<Animator>().SetBool("CastRod", true);
        Invoke("ResetCast", 0.1f);
        stopFishing = false;
        rod.GetComponent<Animator>().SetBool("IsFishing", true);
        isFishingBool = true;
    }

    private void CatchFish()
    {
        isFishingBool = false;
        rod.GetComponent<Animator>().SetBool("IsFishing", false);
        playerInput.SwitchCurrentActionMap("Boating");
        GameStates.SwitchBoatState(player);

        FishIcon.gameObject.SetActive(true);
        FishIcon.sprite = chosenFish.icon;
        fishStats.text = "Fish; " + chosenFish.fishName + " Length: " + chosenFish.fishLength + " Points " + chosenFish.points;
        if (player == 0)
            Inventory.collectFishActionP1.Invoke(chosenFish);
        else if (player == 1)
            Inventory.collectFishActionP2.Invoke(chosenFish);

        fishingSlider.gameObject.SetActive(false);
        targetSlider.gameObject.SetActive(false);
        stopFishing = true;
        catchFx.Play();
    }

    IEnumerator MoveRandomizer()
    {
        while (true)
        {
            if (chosenFish != null)
            {
                moveSpeed = Random.Range(targetSpeeds[chosenFish.fishLength - 1].x, targetSpeeds[chosenFish.fishLength - 1].y);
            }
            if (Random.Range(0, 2) == 0)
                moveSpeed = -moveSpeed;
            yield return new WaitForSeconds(Random.Range(targetSwitchTiming.x, targetSwitchTiming.y));
        }
    }

    public Fish GetRandomFish()
    {
        chosenFish = fishList[Random.Range(0, fishList.Count)];
        return chosenFish;
    }

    private void ResetCast()
    {
        rod.GetComponent<Animator>().SetBool("CastRod", false);
    }
}
