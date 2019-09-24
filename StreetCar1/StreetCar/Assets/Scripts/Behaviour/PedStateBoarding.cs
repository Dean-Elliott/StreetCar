using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class PedStateBoarding : StateBehaviour
{
    GameObjectVar threatVar;
    public FloatVar speedVar;
    public List<GameObject> waypoints;
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
    private Transform door;

    // Called when the state is enabled
    void OnEnable()
    {
        door = FindObjectOfType<PlayerController>().transform.Find("Door");
        Debug.Log("Start boarding");
        speedVar = GetComponent<Blackboard>().GetFloatVar("movementSpeed");


    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stop Waiting");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, door.position, Time.deltaTime * speedVar.Value);
        if ((door.position - transform.position).sqrMagnitude < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}


