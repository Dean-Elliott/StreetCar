using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BirdStateLand : StateBehaviour
{

    public List<GameObject> waypoints;
    public int waypointIndex;
    public float speed;

    public float increaseRate;

    FloatVar energyVar;

    void Awake()
    {
        energyVar = blackboard.GetFloatVar("energy");

    }

    // Called when the state is enabled
    void OnEnable()
    {
        Debug.Log("Started " + GetType().Name);
        waypointIndex = Random.Range(0, waypoints.Count);
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped " + GetType().Name);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardWaypoint();
        IncreaseEnergy();
    }




    void IncreaseEnergy()
    {
        energyVar.Value += increaseRate * Time.deltaTime;
        if (energyVar.Value > 100)
        {
            energyVar.Value = 100;
            SendEvent("EnergyFull");
        }
    }

    void MoveTowardWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        SendEvent("NearbyThreat");
    }
}


