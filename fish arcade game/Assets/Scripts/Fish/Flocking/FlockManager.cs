
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [Header("Flock Settings")]
    public float boundsSize = 5f;
    public float avoidanceDistance, cohesionDistance, alignmentDistance;
    [SerializeField] GameObject fishPrefab;
    [SerializeField] int flockSize = 4;
    [Header("Behaviour Weights")]
    [Range(0, 10)] public float cohesionWeight, avoidanceWeight, alignmentWeight, boundsWeight;
    List<Transform> fishList = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 random = new Vector3(Random.Range(-1f, 1f), Random.Range(-3, 0), Random.Range(-1f, 1f));
            Transform fish = Instantiate(fishPrefab, transform, worldPositionStays: false).transform;
            fishList.Add(fish);
            fish.transform.position += random;
        }

        for (int i = 0; i < fishList.Count; i++)
            fishList[i].GetComponent<FlockFish>().SetFishList(fishList);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, boundsSize);
    }
#endif
}
