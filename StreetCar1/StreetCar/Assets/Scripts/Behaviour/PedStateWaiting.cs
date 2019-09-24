using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class PedStateWaiting : StateBehaviour
{
    public float reactionTime = 1f;

    private PlayerController train;
    private float timeNearby;
    private Vector3 waitingPoint;
    private FloatVar movementSpeed;

    void Awake()
    {
        train = FindObjectOfType<PlayerController>();
    }

    // Called when the state is enabled
    void OnEnable()
    {
        Debug.Log("epic");
        movementSpeed = GetComponent<Blackboard>().GetFloatVar("movementSpeed");

        //create wiating point
        waitingPoint = GameObject.Find("Bus Stop Position").transform.position;
        waitingPoint.z += Random.Range(-1f, 1f);
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped *State*");
    }

    // Update is called once per frame
    void Update()
    {
        //get to the waiting point first
        float sqrDistance = (waitingPoint - transform.position).sqrMagnitude;
        if (sqrDistance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitingPoint, Time.deltaTime * movementSpeed.Value);
            return;
        }

        //if the train isnt moving and close enough
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.5f)
        {
            sqrDistance = (train.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance <= 6f * 6f)
            {
                timeNearby += Time.deltaTime;
                if (timeNearby >= reactionTime)
                {
                    //enter the train
                    SendEvent("SeePlayer");
                    timeNearby = 0f;
                }
            }
        }
    }
}