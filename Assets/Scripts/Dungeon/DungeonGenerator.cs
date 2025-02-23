using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeon_generation_data;
    private List<Vector2Int> dungeon_rooms;


    private void Start()
    {
        dungeon_rooms = DungeonCrawlerController.GenerateDungeon(dungeon_generation_data);
        SpawnRooms(dungeon_rooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int room_location in rooms)
        {

            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), room_location.x, room_location.y);

        }
    }
}
