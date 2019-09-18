using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterNavigationController : MonoBehaviour
{

    public bool reachedDestination = false;
    private Transform waypoint;
    public float movementSpeed = 1.0f;
    public float rotationSpeed = 120f;
    public float stopDistance = 0.2f;

    CharacterNavigationController controller;
    public Waypoint currentWaypoint;
    int direction;

    Vector3 lastPositon;
    Vector3 velocity;
    Vector3 destination;
    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();



    }
    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != destination)
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
        if (controller.reachedDestination)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}

