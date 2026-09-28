using UnityEngine;

public class SetY : MonoBehaviour
{
    [SerializeField] private float targY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, targY, transform.position.z);
        }
    }
}
