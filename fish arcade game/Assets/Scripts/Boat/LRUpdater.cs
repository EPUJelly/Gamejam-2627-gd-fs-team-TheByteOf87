using UnityEngine;

public class LRUpdater : MonoBehaviour
{
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<LineRenderer>().SetPosition(0,p1.position);
        this.GetComponent<LineRenderer>().SetPosition(1, p2.position);
    }
}
