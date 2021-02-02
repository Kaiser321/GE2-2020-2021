using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoid : MonoBehaviour
{
    [Header("Boid Attributes")]
    public Vector3 velocity;
    public float speed;
    public Vector3 acceleration;
    public Vector3 force;
    public float maxSpeed = 5;
    public float maxForce = 10;
    public float mass = 1;

    [Header("Seek")]
    public bool seekEnabled = true;
    public Transform seekTargetTransform;
    public Vector3 seekTarget;

    [Header("Arrive")]
    public bool arriveEnabled = false;
    public Transform arriveTargetTransform;
    public Vector3 arriveTarget;
    public float slowingDistance = 10;

    [Header("Flee")]
    public bool fleeEnabled = false;
    public int fleeDistance;
    public Transform fleeTargetTransform;
    public Vector3 fleeTarget;

    [Header("Path Following")]
    public bool followPath;
    public Path path;


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + acceleration);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + force * 10);

        if (arriveEnabled)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(arriveTargetTransform.position, slowingDistance);
        }

    }

    public Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return (desired - velocity);
    }

    public Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dist) * clamped;

        return desired - velocity;
    }

    public Vector3 Flee(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) < fleeDistance)
        {
            return -Seek(target);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 CalculateForce()
    {
        Vector3 f = Vector3.zero;
        if (seekEnabled)
        {
            if (seekTargetTransform != null)
            {
                seekTarget = seekTargetTransform.position;
            }
            f += Seek(seekTarget);
        }

        if (arriveEnabled)
        {
            if (arriveTargetTransform != null)
            {
                arriveTarget = arriveTargetTransform.position;
            }
            f += Arrive(arriveTarget);
        }

        if (fleeEnabled)
        {
            if (fleeTargetTransform != null)
            {
                fleeTarget = fleeTargetTransform.position;
            }
            f += Flee(fleeTarget);
        }

        return f;
    }

    void FollowPath()
    {
        if(Vector3.Distance(transform.position, path.waypoints[path.currIndex]) < 5)
        {
            if (path.isLooped)
            {
                path.currIndex = (path.currIndex + 1) % path.waypoints.Count;
            }
            else if (path.currIndex + 1 < path.waypoints.Count)
            {
                path.currIndex++;
            }
            seekTarget = path.waypoints[path.currIndex];
        }
    }

    void Update()
    {
        if (followPath)
        {
            FollowPath();
        }

        force = CalculateForce();
        acceleration = force / mass;
        velocity = velocity + acceleration * Time.deltaTime;
        transform.position = transform.position + velocity * Time.deltaTime;
        speed = velocity.magnitude;

        if (speed > 0)
        {
            transform.forward = velocity;
        }
    }
}
