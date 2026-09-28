
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
    [SerializeField] Vector2 targetSpeeds = new Vector2(20f, 50f); // The minimmum and maximum speed of the target
    [SerializeField] Vector2 targetSwitchTiming = new Vector2(0.1f, 1f);
    [SerializeField] int target;
    [SerializeField] int player;
    [SerializeField] float fishHealth = 3f;
    [SerializeField] private Image FishIcon;
    [SerializeField] private TextMeshProUGUI fishStats;
    [SerializeField] PlayerInput playerInput;
    float[] catchSizes = {25, 20, 16, 12, 8};
    float moveSpeed;
    bool stopFishing = true;
    public List<Fish> fishList = new List<Fish>();
    private Fish chosenFish;

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

    private void Update()
    {
        if (stopFishing) return;

        // Start catching fish if hook within target bounds
        if (fishingSlider.value < targetSlider.value + catchSizes[target] / 2 &&
            fishingSlider.value > targetSlider.value - catchSizes[target] / 2)
            fishHealth -= Time.deltaTime;

        if (fishHealth <= 0) CatchFish();

        // Goofy code to keep the target image within bounds
        if (targetSlider.value + moveSpeed * Time.deltaTime > targetSlider.maxValue - catchSizes[target] / 2)
            return;
        else if (targetSlider.value + moveSpeed * Time.deltaTime < targetSlider.minValue + catchSizes[target] / 2)
            return;

        targetSlider.value += moveSpeed * Time.deltaTime;
    }

    private void FishingFish(int playerNumber)
    {
        if (playerNumber != player) return;

        playerInput.SwitchCurrentActionMap("Fishing");
        fishingSlider.gameObject.SetActive(true);
        targetSlider.gameObject.SetActive(true);
        fishHealth = 3f;
        stopFishing = false;
    }

    private void CatchFish()
    {
        playerInput.SwitchCurrentActionMap("Boating");
        GameStates.SwitchBoatState(player);
        chosenFish = GetRandomFish();

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
    }

    IEnumerator MoveRandomizer()
    {
        while (true)
        {
            moveSpeed = Random.Range(targetSpeeds.x, targetSpeeds.y);

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
}
