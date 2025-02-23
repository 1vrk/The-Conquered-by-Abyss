using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBookManager player_movement = player.GetComponent<PlayerBookManager>();
            player_movement.ReplaceBook(gameObject);
            Destroy(gameObject);
        }
    }
}
