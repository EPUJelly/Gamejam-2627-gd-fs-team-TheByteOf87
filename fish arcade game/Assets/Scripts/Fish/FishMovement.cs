using System.Collections;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Fish Stats")]
    [SerializeField] float fishSpeed = 5;
    [SerializeField] float rotationSpeed = 1;
    [Header("Prank em John")]
    [SerializeField] bool isPrankFish;
    [SerializeField] float chanceToPrank = 0.10f;
    float zoneSize;
    bool inZone;
    Vector3 random;
    Vector3 direction;
    Quaternion lookRotation;
    Transform[] boats = new Transform[2];
    bool isPranking;

    private void Start()
    {
        zoneSize = GetComponentInParent<FishingZoneSize>().zoneSize;
        lookRotation = transform.rotation;
        for (int i = 0; i < 2; i++)
            boats[i] = GameObject.FindGameObjectsWithTag("Player")[i].transform;

        if (isPrankFish)
        {
            StartCoroutine(PrankRandomizer());
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * fishSpeed * Time.deltaTime;

        if (isPranking) return;

        TurnBack();
    }

    void TurnBack()
    {
        direction = transform.parent.position - transform.position;
        if (direction.magnitude > zoneSize)
        {
            direction += random;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            if (!inZone) return;

            inZone = false;
            random = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }
        else inZone = true;
    }

    IEnumerator PrankRandomizer()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (Random.Range(0f, 1f) > chanceToPrank) continue;
            float[] distances = { Vector3.Distance(transform.position, boats[0].position), Vector3.Distance(transform.position, boats[1].position) };
            Debug.Log(distances[0].ToString() + " " + distances[1].ToString());
            if (distances[0] > 15f && distances[1] > 15f) continue;
            Debug.Log("b");

            isPranking = true;
            if (distances[0] < distances[1])
                direction = boats[0].position - transform.position;
            else
                direction = boats[1].position - transform.position;

            lookRotation = Quaternion.LookRotation(direction);
        }
    }

    public Vector3 GetDirection()
    {
        return transform.position + direction;
    }

    // Over engineered method to ensure that fish precisly reaches desired direction
    // Not worth it do not use
    //void TurnBackPrecise()
    //{
    //    if (Mathf.Abs(Quaternion.Dot(lookRotation, transform.rotation)) < 0.99f)
    //    {
    //        direction = transform.parent.position - transform.position + random;
    //        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    }

    //    if (Vector3.Distance(transform.parent.position, transform.position) > zoneSize)
    //    {
    //        if (!inZone) return;

    //        inZone = false;
    //        random = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    //        direction = transform.parent.position - transform.position + random;
    //        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    }
    //    else inZone = true;
    //}

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction);
        Gizmos.DrawSphere(transform.parent.position + random, 0.25f);
    }
#endif
}
