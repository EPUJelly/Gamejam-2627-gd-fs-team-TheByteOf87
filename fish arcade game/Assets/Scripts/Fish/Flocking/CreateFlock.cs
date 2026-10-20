
using UnityEngine;
using System.Collections.Generic;


public class CreateFlock : MonoBehaviour
{
    [SerializeField] GameObject fishPrefab;
    [SerializeField] GameObject leaderFish;
    [SerializeField] int flockSize = 4;
    List<Transform> fishList = new List<Transform>();

    private void Start()
    {
        GameObject leader = Instantiate(leaderFish, transform, worldPositionStays: false);

        for (int i = 0; i < flockSize; i++)
        {
            Vector3 random = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Transform fish = Instantiate(fishPrefab, transform, worldPositionStays: false).transform;
            fishList.Add(fish);
            fish.GetComponent<FlockFish>().SetLeader(leader.transform);
            fish.transform.position += random;
        }

        for (int i = 0; i < fishList.Count; i++)
            fishList[i].GetComponent<FlockFish>().SetFishList(fishList);
    }
}
