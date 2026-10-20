
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FlockFish : MonoBehaviour
{
    [Header("Fish Stats")]
    [SerializeField] float fishSpeed = 5;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float avoidDistance = 2.5f;
    Quaternion lookRotation;
    Vector3 direction;
    Vector3 avoidVector;
    Vector3 leaderDirection;
    Vector3 random;
    Transform leader;
    List<Transform> fishList;

    private void Start()
    {
        lookRotation = transform.rotation;
        random = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * fishSpeed * Time.deltaTime;

        avoidVector = new Vector3(0, 0, 3f);
        Vector3 difference;
        float delta = 0;
        foreach (var fish in fishList)
        {
            difference = transform.position - fish.position;
            delta = difference.sqrMagnitude;

            if (delta > avoidDistance)
                continue;

            if (delta < avoidVector.magnitude)
                avoidVector = difference;
        }

        if (avoidVector.sqrMagnitude > avoidDistance)
            Follow();
        else
            Avoid();
    }

    void Follow()
    {
        leaderDirection = leader.GetComponent<FishMovement>().GetDirection();
        direction = leaderDirection - transform.position + random;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    }

    void Avoid()
    {
        lookRotation = Quaternion.LookRotation(new Vector3(avoidVector.x, 0, avoidVector.z));
    }

    public void SetLeader(Transform _leader)
    {
        leader = _leader;
    }

    public void SetFishList(List<Transform> _fishList)
    {
        fishList = new List<Transform>(_fishList);
        fishList.Remove(transform);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + avoidVector);
    }
#endif
}
