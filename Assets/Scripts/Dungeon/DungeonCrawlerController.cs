using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    top = 0,
    left = 1,
    down = 2,
    right = 3
}

public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> position_visited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> direction_movement = new Dictionary<Direction, Vector2Int>
    {
        {Direction.top, Vector2Int.up },
        {Direction.left, Vector2Int.left },
        {Direction.down, Vector2Int.down },
        {Direction.right, Vector2Int.right }
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeon_data)
    {
        List<DungeonCrawler> dungeon_crawlers = new List<DungeonCrawler>();

        for (int i = 0; i < dungeon_data.number_of_crawlers; i++)
        {
            dungeon_crawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }
        int iterations = Random.Range(dungeon_data.iteration_min, dungeon_data.iteration_max);

        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeon_crawlers)
            {
                Vector2Int new_pos = dungeonCrawler.Move(direction_movement);
                position_visited.Add(new_pos);
            }
        }
        return position_visited;
    }

    public static void ClearVisitedPositions()
    {
        position_visited.Clear();
    }
}