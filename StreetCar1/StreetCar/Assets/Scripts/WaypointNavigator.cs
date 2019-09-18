using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{

    CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    int direction;
    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
        

        
    }
    // Start is called before the first frame update
    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void pathToNextWaypoint()
    {
        if (controller.reachedDestination)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
