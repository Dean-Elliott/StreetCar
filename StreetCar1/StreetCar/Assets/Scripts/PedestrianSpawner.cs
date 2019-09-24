using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public GameObject pedestrianPrefab2;
    public int pedestriansToSpawn;

    private List<GameObject> pedestriansSpawned = new List<GameObject>();

    void Update()
    {
        //clear empty peds from the list
        for (int i = pedestriansSpawned.Count - 1; i >= 0; i--)
        {
            if (!pedestriansSpawned[i])
            {
                pedestriansSpawned.RemoveAt(i);
            }
        }

        if (pedestriansSpawned.Count < pedestriansToSpawn)
        {
            int enemiesLeftToSpawn = pedestriansToSpawn - pedestriansSpawned.Count;
            for (int i = 0; i < enemiesLeftToSpawn; i++)
            {
                GameObject prefab = i % 3 == 0 ? pedestrianPrefab2 : pedestrianPrefab;
                SpawnPed(prefab);
            }
        }
    }

    private void SpawnPed(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));

        Waypoint waypoint = child.GetComponent<Waypoint>();
        WaypointNavigator waypointNavigator = obj.GetComponent<WaypointNavigator>();
        if (waypointNavigator)
        {
            waypointNavigator.currentWaypoint = waypoint;
        }

        CharacterNavigationController navigationController = obj.GetComponent<CharacterNavigationController>();
        if (navigationController)
        {
            navigationController.currentWaypoint = waypoint;
        }

        obj.transform.position = child.position;
        pedestriansSpawned.Add(obj);
    }
}
