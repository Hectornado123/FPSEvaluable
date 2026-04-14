using UnityEngine;

public class Robot : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;

    public float speed = 3f;
    public float runSpeed = 6f;
    public float detectionRange = 8f;
    public float waypointTolerance = 0.5f;
    public float runDistance = 10f;

    private int currentWaypoint = 0;
    private Vector3 targetPosition;
    private bool isRunning = false;

    void Start()
    {
        if (waypoints.Length > 0)
            targetPosition = waypoints[0].position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isRunning = true;
            RunAway();
        }
        else
        {
            isRunning = false;
            Patrol();
        }

        Move();
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        if (Vector3.Distance(transform.position, targetPosition) < waypointTolerance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            targetPosition = waypoints[currentWaypoint].position;
        }
    }

    void RunAway()
    {
        Vector3 directionAway = (transform.position - player.position).normalized;
        targetPosition = transform.position + directionAway * runDistance;
    }

    void Move()
    {
        float currentSpeed = isRunning ? runSpeed : speed;

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Rotación hacia donde va
        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);
        }
    }
}