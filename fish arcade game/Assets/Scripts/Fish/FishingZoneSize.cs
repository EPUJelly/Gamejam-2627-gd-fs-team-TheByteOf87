using UnityEditor;
using UnityEngine;

public class FishingZoneSize : MonoBehaviour
{
    public float zoneSize = 18f;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, zoneSize);
    }
#endif
}
