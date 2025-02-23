using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float vertical_offset, horizontal_offset;
    }

    public Grid grid;

    public GameObject grid_tile;

    public List<Vector2> available_points = new List<Vector2>();

    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 2;
        grid.rows = room.Height - 3;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid.vertical_offset += room.transform.localPosition.y;
        grid.horizontal_offset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(grid_tile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontal_offset), y - (grid.rows - grid.vertical_offset));
                go.name = "X: " + x + ", Y: " + y;
                available_points.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
