using System.Collections.Generic;
using UnityEngine;

public class RoomNode {
    public Vector2Int GridPos;
    public RoomType Type = RoomType.Normal;
    public bool Visited;
    public bool Cleared;

    // which directions have doors
    public Dictionary<Direction, bool> Doors = new() {
        { Direction.North, false },
        { Direction.South, false },
        { Direction.East, false },
        { Direction.West, false },
    };

    public RoomNode(Vector2Int pos) {
        GridPos = pos;
    }
}