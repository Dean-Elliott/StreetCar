using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class GuardStateChasing : StateBehaviour {

    public float speed;
    GameObjectVar threatVar;

    void Awake() {
        threatVar = blackboard.GetGameObjectVar("threat");
    }

    // Called when the state is enabled
    void OnEnable() {
        Debug.Log("Starting Chasing");

    }

    // Called when the state is disabled
    void OnDisable() {
        Debug.Log("Stopping Chasing");
    }

    void Update() {
        MoveTowardThreat();
    }


    void MoveTowardThreat() {
        transform.position = Vector3.MoveTowards(transform.position, threatVar.Value.transform.position, speed * Time.deltaTime);
    }



    void OnVisionExit(Collider other) {
        Debug.Log("Lost Player!!!!");
        SendEvent("LostSightOfPlayer");
        threatVar.Value = null;
    }


}
