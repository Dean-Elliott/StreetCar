using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using System.Threading.Tasks;

public class WeatherStateClear : StateBehaviour
{
	// Called when the state is enabled
	async void OnEnable () {

        Blackboard blackboard = GetComponent<Blackboard>();
		Debug.Log("Started *State*");

        float randomTime = Random.Range(14f, 35f);
        await Task.Delay((int)(randomTime * 1000f));

        bool wasFoggy = blackboard.GetBoolVar("WasWeatherFoggy").Value;
        if (wasFoggy)
        {
            blackboard.GetBoolVar("WasWeatherFoggy").Value = false;
            SendEvent("Cloudy");
        }
        else
        {
            blackboard.GetBoolVar("WasWeatherFoggy").Value = true;
            SendEvent("FogIncoming");
        }

        //clear
        //rain
        //clear
        //fog
    }
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
}


