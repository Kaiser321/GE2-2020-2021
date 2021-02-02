using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> waypoints;
    public bool isLooped;
    public int currIndex = 0;

    void OnDrawGizmos()
    {
        for (int i = 0; i <= waypoints.Count -1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(waypoints[i], 2);
            Gizmos.color = Color.red;
            if (isLooped)
            {
                Gizmos.DrawLine(waypoints[i], waypoints[(i+1) % waypoints.Count]);
            }
            else if (i + 1 < waypoints.Count)
            {
                Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            }
        }
    }

    void Awake()
    {
        waypoints.Add(transform.position);
        waypoints.Add(new Vector3(-20, 0, 0));
        waypoints.Add(new Vector3(-20, 0, 20));
        waypoints.Add(new Vector3(0, 0, 20));
    }
}
