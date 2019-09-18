using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class GuardStateChasing : StateBehaviour {

    GameObjectVar threatVar;
    public FloatVar speedVar;

    void Awake() {
        threatVar = blackboard.GetGameObjectVar("threat");
    }

    // Called when the state is enabled
    void OnEnable() {
        Debug.Log("Starting Chasing");
           speedVar = blackboard.GetFloatVar ("speed");
}

    // Called when the state is disabled
    void OnDisable() {
        Debug.Log("Stopping Chasing");
    }

    void Update() {
        MoveTowardThreat();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == threatVar.Value)
        {
            OnVisionExit(other);
        }
    }


    void MoveTowardThreat() {
        transform.position = Vector3.MoveTowards(transform.position, threatVar.Value.transform.position, speedVar.Value * Time.deltaTime);
    }



    void OnVisionExit(Collider other) {
        Debug.Log("Lost Player!!!!");
        SendEvent("LostSightOfPlayer");
        threatVar.Value = null;
    }


}
