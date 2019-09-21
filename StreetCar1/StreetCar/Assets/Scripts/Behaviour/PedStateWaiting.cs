using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class PedStateWaiting : StateBehaviour
{

    void Awake()
    {
       


        waypointParentVar = blackboard.GetGameObjectVar("waypointParent");

        if (waypointParentVar.Value.transform.childCount > waypoints.Count)
        {
            for (int i = 0; i < waypointParentVar.Value.transform.childCount; i++)
            {
                waypoints.Add(waypointParentVar.Value.transform.GetChild(i).gameObject);
            }
        }

    }
    PlayerController PlayerController = FindObjectOfType<PlayerController>();
    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("epic");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == waypoints[waypointIndex])
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }

        else if (other.tag == "Player")
        {
            OnVisionEnter(other);
        }
    }

    void OnVisionEnter(Collider other)
    {
        PlayerController  = other.gameObject;
        SendEvent("SeePlayer");
    }


