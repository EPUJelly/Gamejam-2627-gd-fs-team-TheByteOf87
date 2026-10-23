

using System.Collections.Generic;
using UnityEngine;

public class FlockFish : MonoBehaviour
{
    [Header("Fish Stats")]
    [SerializeField] float fishSpeed = 5;
    [SerializeField] float rotationSpeed = 3;
    [SerializeField] float FOV;
    [SerializeField] Vector2 heightLimits;
    FlockManager flock;
    Quaternion lookRotation;
    Vector3 moveVector;
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

        //Debug.DrawLine(transform.position, transform.position + moveVector, Color.white);
        //Debug.DrawLine(transform.position, transform.position + cohesionVector, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + alignmentVector, Color.green);
        //Debug.DrawLine(transform.position, transform.position + avoidanceVector, Color.purple);
        //Debug.DrawLine(transform.position, transform.position + boundsVector, Color.red);
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
                cohesionVector += fish.position - transform.position;
        }

        return cohesionVector.normalized;
    }

    Vector3 CalculateAlignmentVector()
    {
        if (alignmentNeighbours.Count == 0) return transform.forward;

        var alignmentVector = transform.forward;
        foreach (var fish in alignmentNeighbours)
        {
                alignmentVector += fish.forward;
        }

        return alignmentVector.normalized;
    }

    Vector3 CalculateAvoidVector()
    {
        if (avoidanceNeighbours.Count == 0) return Vector3.zero;

        Vector3 avoidVector = Vector3.zero;
        foreach (var fish in avoidanceNeighbours)
        {
                avoidVector += transform.position - fish.position;
        }

        return avoidVector.normalized;
    }

    private Vector3 CalculateBoundsVector()
    {
        if (transform.position.y - flock.transform.position.y > heightLimits.y)
            return Vector3.down;
        else if (transform.position.y - flock.transform.position.y < heightLimits.x)
            return Vector3.up;

        var offset = flock.transform.position - transform.position;

        return offset / flock.boundsSize;
    }

    public void SetFishList(List<Transform> _fishList)
    {
        fishList = new List<Transform>(_fishList);
        fishList.Remove(transform);
    }
}
