using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawner_data;

    }
    
    public GridController grid;
    public RandomSpawner[] random_spawner;

    private void Start()
    {
        //grid = GetComponentInChildren<GridController>();
    }

    public void InitialiseObjectSpawning()
    {
        foreach(RandomSpawner rs in random_spawner)
        {
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(RandomSpawner data)
    {
        int random_iteration = Random.Range(data.spawner_data.min_spawn, data.spawner_data.max_spawn + 1);

        if(grid != null)
        {
            for (int i = 0; i < random_iteration; i++)
            {
                int random_position = Random.Range(0, grid.available_points.Count - 1);
                GameObject go = Instantiate(data.spawner_data.iem_to_spawn, grid.available_points[random_position], Quaternion.identity, transform) as GameObject;
                grid.available_points.RemoveAt(random_position);
                Debug.Log("Spawned object");

            }
        }
        else
        {
            GameObject go = Instantiate(data.spawner_data.iem_to_spawn, new Vector3(0.07f, 1.301f, 0), Quaternion.identity, transform) as GameObject;
         
        }
       
    }
}
