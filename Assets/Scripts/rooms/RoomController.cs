using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Wall blockers — active when there is NO door in that direction")]
    public GameObject northWall;
    public GameObject southWall;
    public GameObject eastWall;
    public GameObject westWall;

    public void SetDoors(Dictionary<Direction, bool> doors)
    {
        SetWall(northWall, doors, Direction.North);
        SetWall(southWall, doors, Direction.South);
        SetWall(eastWall, doors, Direction.East);
        SetWall(westWall, doors, Direction.West);
    }

    void SetWall(GameObject wall, Dictionary<Direction, bool> doors, Direction dir)
    {
        if (wall == null) return;
        bool hasDoor = doors.TryGetValue(dir, out bool open) && open;
        wall.SetActive(!hasDoor);
    }
}
