using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;


public class GuardStatePatrolling : StateBehaviour {

    public List<GameObject> waypoints;
    public int waypointIndex;
    public FloatVar speedVar;

    public float depletionRate;

    FloatVar energyVar;
    GameObjectVar threatVar;
    GameObjectVar waypointParentVar;

    void Awake() {
        energyVar = blackboard.GetFloatVar("energy");
        threatVar = blackboard.GetGameObjectVar("threat");
        speedVar = blackboard.GetFloatVar("speed");


        waypointParentVar = blackboard.GetGameObjectVar("waypointParent");

        if (waypointParentVar.Value.transform.childCount > waypoints.Count) {
            for (int i = 0; i < waypointParentVar.Value.transform.childCount; i++)
            {
                waypoints.Add(waypointParentVar.Value.transform.GetChild(i).gameObject);
            }
        }
 
    }

    // Called when the state is enabled
    void OnEnable() {
        Debug.Log("Starting Patrolling");
    }

    // Called when the state is disabled
    void OnDisable() {
        Debug.Log("Stopping Patrolling");
    }

    void Update() {
        MoveTowardWaypoint();
    
    }

    void waitDetermine() {
       
        if (Random.value > 0.5f) {
            energyVar.Value = 0;
            SendEvent("moveToBusStop");
        }
    }

    void MoveTowardWaypoint() {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speedVar.Value * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == waypoints[waypointIndex]) {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }

        else if (other.tag == "Player")
        {
            OnVisionEnter(other);
        }
    }

    void OnVisionEnter(Collider other) {
        threatVar.Value = other.gameObject;
        SendEvent("SeePlayer");
    }

}
