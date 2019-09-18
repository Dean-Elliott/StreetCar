using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;


public class GuardStatePatrolling : StateBehaviour {

    public List<GameObject> waypoints;
    public int waypointIndex;
    public float speed;

    public float depletionRate;

    FloatVar energyVar;
    GameObjectVar threatVar;

    void Awake() {
        energyVar = blackboard.GetFloatVar("energy");
        threatVar = blackboard.GetGameObjectVar("threat");
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
        DepleteEnergy();
    }

    void DepleteEnergy() {
        energyVar.Value -= depletionRate * Time.deltaTime;
        if (energyVar.Value <= 0) {
            energyVar.Value = 0;
            SendEvent("Tired");
        }
    }

    void MoveTowardWaypoint() {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == waypoints[waypointIndex]) {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
    }

    void OnVisionEnter(Collider other) {
        threatVar.Value = other.gameObject;
        SendEvent("SeePlayer");
    }

}
