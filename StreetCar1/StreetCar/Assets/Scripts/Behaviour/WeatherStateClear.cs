using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class WeatherStateClear : StateBehaviour
{
	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started *State*");
        SendEvent(1833751859);
    }
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


