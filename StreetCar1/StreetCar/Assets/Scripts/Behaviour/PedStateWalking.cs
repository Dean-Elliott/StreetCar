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

    public GameObjectVar waypointParent;

    public Waypoint currentWaypoint;
    int direction;

    public GameObjectVar waitTransitionWaypoint;

    Vector3 lastPositon;
    Vector3 velocity;
    Vector3 destination;
    // Start is called before the first frame update

    private void OnEnable()
    {
        blackboard = GetComponent<Blackboard>();

        waypointParent = blackboard.GetGameObjectVar("waypointParent");

        waitTransitionWaypoint = blackboard.GetGameObjectVar("busStopDecisionWaypoint");

        movementSpeed = blackboard.GetFloatVar("movementSpeed");
        stopDistance = blackboard.GetFloatVar("stopDistance");
        rotationSpeed = blackboard.GetFloatVar("rotationSpeed");

        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        float tempSqrMagnitude = 999999999999999;
        for(int i = 0; i < waypointParent.Value.transform.childCount; i++)
        {
            if((waypointParent.Value.transform.GetChild(i).position - transform.position).sqrMagnitude < tempSqrMagnitude)
            {
                tempSqrMagnitude = (waypointParent.Value.transform.GetChild(i).position - transform.position).sqrMagnitude;
                currentWaypoint = waypointParent.Value.transform.GetChild(i).GetComponent<Waypoint>();
            }
        }


        SetDestination(currentWaypoint.GetPosition());
    }

   void shouldIWait()
    {
        if (Random.value > 0.9f)
        {         
            SendEvent("ApproachBusStop");
        }
    }

    // Update is called once per frame
    void Update()
    {
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

            if (currentWaypoint.gameObject == waitTransitionWaypoint.Value)
            {
                print("buh");
                shouldIWait();
            }
            else if (direction == 0)
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




