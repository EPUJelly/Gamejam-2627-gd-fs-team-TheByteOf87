using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5;
    [SerializeField] float roationSpeed = 1;
    Quaternion lookRotation;

    private void Start()
    {
        lookRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, roationSpeed * Time.deltaTime);
        transform.position += transform.forward * fishSpeed * Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        var direction = Vector3.Normalize(other.transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-1f, 1f)) - transform.position);
        lookRotation = Quaternion.LookRotation(direction);
    }
}
