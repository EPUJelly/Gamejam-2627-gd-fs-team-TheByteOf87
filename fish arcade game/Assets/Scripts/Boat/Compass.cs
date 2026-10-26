using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private List<Transform> points;
    private List<float> distances = new List<float>();
    private Transform closestTarg;
    [SerializeField] private Transform lap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < points.Count; i++)
        {
            distances.Insert(i, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distances.Clear();
        for (int i = 0; i < points.Count; i++)
        {
            distances.Insert(i,FindDistance(points[i]));
        }
        distances.Sort();
        for (int i = 0; i < distances.Count; i++)
        {
            if (distances[0] == FindDistance(points[i]))
            {
                closestTarg = points[i];
            }
        }
        lap.LookAt(closestTarg);
        arrow.rotation = Quaternion.Euler(0,lap.rotation.eulerAngles.y + 90,-90);
    }

    private float FindDistance(Transform targ)
    {
        float dist = Vector3.Distance(this.transform.position,targ.position);
        return dist;
    }
}
