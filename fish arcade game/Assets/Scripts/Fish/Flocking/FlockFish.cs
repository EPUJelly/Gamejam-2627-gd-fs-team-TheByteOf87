
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class FlockFish : MonoBehaviour
{
    [Header("Fish Stats")]
    [SerializeField] float fishSpeed = 5;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float avoidDistance = 2.5f;
    FlockManager flock;
    Quaternion lookRotation;
    Vector3 moveVector;
    float FOV;
    List<Transform> fishList = new List<Transform>(),
        cohesionNeighbours = new List<Transform>(),
        avoidanceNeighbours = new List<Transform>(), 
        alignmentNeighbours = new List<Transform>();

    private void Start()
    {
        flock = transform.parent.GetComponent<FlockManager>();
        lookRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * fishSpeed * Time.deltaTime;

        GetNearbyFish();
        var cohesionVector = CalculateCohesionVector() * flock.cohesionWeight;
        var avoidanceVector = CalculateAvoidVector() * flock.avoidanceWeight;
        var alignmentVector = CalculateAlignmentVector() * flock.alignmentWeight;
        var boundsVector = CalculateBoundsVector() * flock.boundsWeight;

        moveVector = cohesionVector + avoidanceVector + alignmentVector + boundsVector;

        if (moveVector == Vector3.zero)
            moveVector = transform.forward;

        Debug.DrawLine(transform.position, transform.position + moveVector, Color.white);
        lookRotation = Quaternion.LookRotation(moveVector);
    }

    void GetNearbyFish()
    {
        cohesionNeighbours.Clear();
        avoidanceNeighbours.Clear();
        alignmentNeighbours.Clear();

        foreach (Transform fish in fishList)
        {
            float distance = (transform.position - fish.position).sqrMagnitude;

            if (distance <= flock.cohesionDistance * flock.cohesionDistance)
                cohesionNeighbours.Add(fish);

            if (distance <= flock.avoidanceDistance * flock.avoidanceDistance)
                avoidanceNeighbours.Add(fish);

            if (distance <= flock.alignmentDistance * flock.alignmentDistance)
                alignmentNeighbours.Add(fish);
        }
    }

    Vector3 CalculateCohesionVector()
    {
        if (cohesionNeighbours.Count == 0) return Vector3.zero;

        Vector3 cohesionVector = Vector3.zero;
        foreach (var fish in cohesionNeighbours)
        {
            if (Vector3.Angle(transform.position, fish.position) <= FOV)
                cohesionVector += fish.position;
        }

        Debug.DrawLine(transform.position, transform.position + cohesionVector, Color.blue);
        return cohesionVector.normalized;
    }

    Vector3 CalculateAlignmentVector()
    {
        if (alignmentNeighbours.Count == 0) return transform.forward;

        var alignmentVector = transform.forward;
        foreach (var fish in alignmentNeighbours)
        {
            if (Vector3.Angle(transform.position, fish.position) <= FOV)
                alignmentVector += fish.forward;
        }

        Debug.DrawLine(transform.position, transform.position + alignmentVector, Color.green);
        return alignmentVector.normalized;
    }

    Vector3 CalculateAvoidVector()
    {
        if (avoidanceNeighbours.Count == 0) return Vector3.zero;

        Vector3 avoidVector = Vector3.zero;
        foreach (var fish in avoidanceNeighbours)
        {   
            if (Vector3.Angle(transform.position, fish.position) <= FOV)
                avoidVector += transform.position - fish.position;
        }

        Debug.DrawLine(transform.position, transform.position + avoidVector, Color.blue);
        return avoidVector.normalized;
    }

    private Vector3 CalculateBoundsVector()
    {
        var offset = flock.transform.position - transform.position;
        var nearCenter = offset.magnitude >= flock.boundsSize * 0.9f;

        Debug.DrawLine(transform.position, transform.position + offset, Color.red);
        return nearCenter ? offset.normalized : Vector3.zero;
    }

    public void SetFishList(List<Transform> _fishList)
    {
        fishList = new List<Transform>(_fishList);
        fishList.Remove(transform);
    }
}
