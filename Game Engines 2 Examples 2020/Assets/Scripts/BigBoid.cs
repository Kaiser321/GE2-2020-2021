using UnityEngine;

public class BigBoid : MonoBehaviour
{
    [Header("Boid Attributes")]
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float speed;
    public float maxSpeed = 5;
    public float maxForce = 10;
    public float mass = 1;
    public float banking = 0.1f;
    public float damping = 0.1f;

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
    public Path path;
    public bool pathFollowingEnabled = false;
    public float waypointDistance = 3;

    [Header("Player Steering")]
    public bool playerSteeringEnabled = false;
    public float steeringForce = 100;

    [Header("Pursue")]
    public bool pursueEnabled = false;
    public BigBoid pursueTarget;
    public Vector3 pursueTargetPos;

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

        if (pursueEnabled)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, pursueTargetPos);
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

    public Vector3 PathFollow()
    {
        Vector3 nextWaypoint = path.NextWaypoint();
        if (!path.looped && path.IsLast())
        {
            return Arrive(nextWaypoint);
        }
        else
        {
            if (Vector3.Distance(transform.position, nextWaypoint) < waypointDistance)
            {
                path.AdvanceToNext();
            }
            return Seek(nextWaypoint);
        }
    }

    public Vector3 PlayerSteering()
    {
        Vector3 force = Vector3.zero;

        force += Input.GetAxis("Vertical") * transform.forward * steeringForce;

        Vector3 projectedRight = transform.right;
        projectedRight.y = 0;
        projectedRight.Normalize();

        force += Input.GetAxis("Horizontal") * projectedRight * steeringForce * 0.2f;

        return force;
    }

    public Vector3 Pursue(BigBoid pursueTarget)
    {
        float dist = Vector3.Distance(pursueTarget.transform.position, transform.position);

        float time = dist / maxSpeed;

        pursueTargetPos = pursueTarget.transform.position + pursueTarget.velocity * time;

        return Seek(pursueTargetPos);
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

        if (pathFollowingEnabled)
        {
            f += PathFollow();
        }

        if (playerSteeringEnabled)
        {
            f += PlayerSteering();
        }

        if (pursueEnabled)
        {
            f += Pursue(pursueTarget);
        }

        return f;
    }

    void Update()
    {
        force = CalculateForce();
        acceleration = force / mass;
        velocity = velocity + acceleration * Time.deltaTime;
        transform.position = transform.position + velocity * Time.deltaTime;
        speed = velocity.magnitude;
        if (speed > 0)
        {
            //transform.forward = velocity;

            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);
            //velocity *= 0.9f;

            // Remove 10% of the velocity every second
            velocity -= (damping * velocity * Time.deltaTime);
        }
    }
}
