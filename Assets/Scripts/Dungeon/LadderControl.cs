using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderControl : MonoBehaviour
{
    public GameObject light_ladder;
    private bool in_ladder = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            light_ladder.SetActive(true);
            in_ladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            light_ladder.SetActive(false);
            in_ladder = false;
        }
    }

    private void Update()
    {
        if (in_ladder)
        if (Input.GetKeyDown(KeyCode.E))
        {
                GameController.SaveBalance();
                RoomController.instance.ClearLoadedRooms();
                DungeonCrawlerController.ClearVisitedPositions();
                SceneManager.LoadScene(2);
                
           
        }
    }
}

