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

    void Awake()
    {
        threatVar = blackboard.GetGameObjectVar("threat");
    }

    // Called when the state is enabled
    void OnEnable()
    {
        Debug.Log("Start boarding");
        speedVar = blackboard.GetFloatVar("speed");
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stop Waiting");
    }

    void Update()
    {
        blackboard = GetComponent<Blackboard>();

        waypointParent = blackboard.GetGameObjectVar("waypointParent");

        waitTransitionWaypoint = blackboard.GetGameObjectVar("busStopDecisionWaypoint");

        movementSpeed = blackboard.GetFloatVar("movementSpeed");
        stopDistance = blackboard.GetFloatVar("stopDistance");
        rotationSpeed = blackboard.GetFloatVar("rotationSpeed");

        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        float tempSqrMagnitude = 999999999999999;
        for (int i = 0; i < waypointParent.Value.transform.childCount; i++)
        {
            if ((waypointParent.Value.transform.GetChild(i).position - transform.position).sqrMagnitude < tempSqrMagnitude)
            {
                tempSqrMagnitude = (waypointParent.Value.transform.GetChild(i).position - transform.position).sqrMagnitude;
                currentWaypoint = waypointParent.Value.transform.GetChild(i).GetComponent<Waypoint>();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == threatVar.Value)
        {
            OnVisionExit(other);
        }
    }


    void MoveTowardThreat()
    {
        transform.position = Vector3.MoveTowards(transform.position, threatVar.Value.transform.position, speedVar.Value * Time.deltaTime);
    }



    void OnVisionExit(Collider other)
    {
        Debug.Log("Lost Player!!!!");
        SendEvent("LostSightOfPlayer");
        threatVar.Value = null;
    }
}


