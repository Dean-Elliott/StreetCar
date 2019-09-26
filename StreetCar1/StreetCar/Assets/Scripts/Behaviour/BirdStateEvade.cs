using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BirdStateEvade : StateBehaviour
{

    public List<GameObject> waypoints;
    public int waypointIndex;
    public float speed;

    public float depletionRate;

    FloatVar energyVar;

    void Awake()
    {
        energyVar = blackboard.GetFloatVar("energy");
     
    }

    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("Started "+GetType().Name);
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped " + GetType().Name);
	}

    // Update is called once per frame
    void Update()
    {
        MoveTowardWaypoint();
        DepleteEnergy();
    }




    void DepleteEnergy()
    {
        energyVar.Value -= depletionRate * Time.deltaTime;
        if (energyVar.Value <= 0)
        {
            energyVar.Value = 0;
            SendEvent("EnergyLow");
        }
    }

    void MoveTowardWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);
        if ((transform.position - waypoints[waypointIndex].transform.position).sqrMagnitude <= 0.01f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
    }
}


