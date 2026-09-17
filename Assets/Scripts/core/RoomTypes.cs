using UnityEngine;

public enum Direction { North, South, East, West }

public enum RoomType { Normal, Start, Boss, Treasure, Shop, Secret }

public static class DirectionUtil {
    public static Vector2Int ToOffset(Direction dir) => dir switch {
        Direction.North => new Vector2Int(0, 1),
        Direction.South => new Vector2Int(0, -1),
        Direction.East  => new Vector2Int(1, 0),
        Direction.West  => new Vector2Int(-1, 0),
        _ => Vector2Int.zero
    };

    public static Direction Opposite(Direction dir) => dir switch {
        Direction.North => Direction.South,
        Direction.South => Direction.North,
        Direction.East  => Direction.West,
        Direction.West  => Direction.East,
        _ => dir
    };
}