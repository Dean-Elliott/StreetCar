using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class GuardStateSleeping : StateBehaviour {

    public float regenerationRate;

    FloatVar energyVar;
    FloatVar maxEnergyVar;

    // Called when the state is enabled
    void OnEnable() {
        Debug.Log("Starting Sleeping");
    }

    // Called when the state is disabled
    void OnDisable() {
        Debug.Log("Stopping Sleeping");
    }

    void Awake() {
        energyVar = blackboard.GetFloatVar("energy");
        maxEnergyVar = blackboard.GetFloatVar("maxEnergy");
    }

    void Update() {
        RegenerateEnergy();
    }

    void RegenerateEnergy() {
        energyVar.Value += regenerationRate * Time.deltaTime;
        if (energyVar.Value >= maxEnergyVar.Value) {
            energyVar.Value = maxEnergyVar.Value;
            SendEvent("Rejuvenated");
        }
    }
}
