using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject game_object;

        public float weight;
    }
    public List<Spawnable> items = new List<Spawnable>();

    float total_weight;

    private void Awake()
    {
        total_weight = 0;
        foreach(var spawnable in items)
        {
            total_weight += spawnable.weight;
        }
    }
    void Start()
    {
        float pick = Random.value * total_weight;
        int chosen_index = 0;
        float cumulative_weight = items[0].weight;

        while(pick > cumulative_weight && chosen_index < items.Count-1)
        {
            chosen_index++;
            cumulative_weight += items[chosen_index].weight;
        }

        GameObject i = Instantiate(items[chosen_index].game_object, transform.position, Quaternion.identity, transform) as GameObject;
    }

}
