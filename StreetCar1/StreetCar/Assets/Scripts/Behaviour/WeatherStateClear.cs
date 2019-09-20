using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using System.Threading.Tasks;

public class WeatherStateClear : StateBehaviour
{
	// Called when the state is enabled
	async void OnEnable () {
		Debug.Log("Started *State*");

        float randomTime = Random.Range(14f, 35f);
        await Task.Delay((int)(randomTime * 1000f));

       // SendEvent("FogIncoming");
        SendEvent("Cloudy");
    }
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
}


