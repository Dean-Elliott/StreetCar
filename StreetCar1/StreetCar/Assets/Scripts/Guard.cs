using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guard : MonoBehaviour {

    public enum State {
        Patrolling,
        Napping,
        Chasing
    }

    public State state;


    public List<GameObject> waypoints;
    public int waypointIndex;
    public float patrolSpeed;
    public float chaseSpeed;

    public float energy;
    public float maxEnergy;
    public float energyDepletionRate;
    public float energyRegenerationRate;

    public GameObject threat;

    // Update is called once per frame
    void Update() {

        switch (state) {
            case State.Patrolling:
                Patrol();
                break;
            case State.Napping:
                Nap();
                break;
            case State.Chasing:
                Chase();
                break;
        }
    }


    void Patrol() {
        MoveTowardWaypoint();
        DepleteEnergy();
    }

    void Chase() {
        MoveTowardThreat();
    }

    void Nap() {
        RegenerateEnergy();
    }


    void MoveTowardThreat() {
        transform.position = Vector3.MoveTowards(transform.position, threat.transform.position, chaseSpeed * Time.deltaTime);
    }


    void RegenerateEnergy() {
        energy += energyRegenerationRate * Time.deltaTime;
        if (energy >= maxEnergy) {
            energy = maxEnergy;
            state = State.Patrolling;
        }
    }


    void DepleteEnergy() {
        energy -= energyDepletionRate * Time.deltaTime;
        if (energy <= 0) {
            energy = 0;
            state = State.Napping;
        }
    }


    void MoveTowardWaypoint() {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, patrolSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == waypoints[waypointIndex]) {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
    }


    void OnVisionEnter(Collider other) {
            state = State.Chasing;
            threat = other.gameObject;
    }



    void OnVisionExit(Collider other) {
            Debug.Log("Lost Player!!!!");
            state = State.Patrolling;
            threat = null;
    }

}


