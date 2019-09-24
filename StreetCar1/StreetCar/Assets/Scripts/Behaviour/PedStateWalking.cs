using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class PedStateWalking : StateBehaviour
{

    private Blackboard blackboard;

    public bool reachedDestination = false;
    private Transform waypoint;
    public FloatVar movementSpeed;
    public FloatVar rotationSpeed;
    public FloatVar stopDistance;

    public Waypoint currentWaypoint;
    int direction;

    public GameObjectVar waitTransitionWaypoint;

    Vector3 lastPositon;
    Vector3 velocity;
    Vector3 destination;
    private float timeToCheck;

    // Start is called before the first frame update

    private void OnEnable()
    {
        blackboard = GetComponent<Blackboard>();

        GameObject waypointParent = FindObjectOfType<PedestrianSpawner>().gameObject;

        waitTransitionWaypoint = blackboard.GetGameObjectVar("busStopDecisionWaypoint");

        movementSpeed = blackboard.GetFloatVar("movementSpeed");
        stopDistance = blackboard.GetFloatVar("stopDistance");
        rotationSpeed = blackboard.GetFloatVar("rotationSpeed");

        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        float tempSqrMagnitude = 999999999999999;
        for (int i = 0; i < waypointParent.transform.childCount; i++)
        {
            if ((waypointParent.transform.GetChild(i).position - transform.position).sqrMagnitude < tempSqrMagnitude)
            {
                tempSqrMagnitude = (waypointParent.transform.GetChild(i).position - transform.position).sqrMagnitude;
                currentWaypoint = waypointParent.transform.GetChild(i).GetComponent<Waypoint>();
            }
        }

        timeToCheck = -20f;
        SetDestination(currentWaypoint.GetPosition());
    }

    void shouldIWait()
    {
        if (Random.value < 0.1f)
        {
            SendEvent("ApproachBusStop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToCheck += Time.deltaTime;
        if (timeToCheck > 1f)
        {
            timeToCheck = 0f;
            shouldIWait();
        }

        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            }
            else
            {
                reachedDestination = true;
            }

            velocity = (transform.position - lastPositon) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);
        }

        pathToNextWaypoint();

    }


    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

    void pathToNextWaypoint()
    {
        if (reachedDestination)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }

            SetDestination(currentWaypoint.GetPosition());
        }
    }
}




