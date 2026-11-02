using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Die", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
