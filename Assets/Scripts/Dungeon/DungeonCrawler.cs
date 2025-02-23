using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position {  get;  set; }
    public DungeonCrawler(Vector2Int start_pos)
    {
        Position = start_pos;
    }


    public Vector2Int Move(Dictionary<Direction, Vector2Int> direction_movement_map)
    {
        Direction to_move = (Direction)Random.Range(0, direction_movement_map.Count);
        Position += direction_movement_map[to_move];
        return Position;
    }
}
