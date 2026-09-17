using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {
    public int targetRoomCount = 12;
    public int seed = 0;

    public Dictionary<Vector2Int, RoomNode> Generate() {
        var rng = seed == 0 ? new System.Random() : new System.Random(seed);
        var map = new Dictionary<Vector2Int, RoomNode>();

        var start = new RoomNode(Vector2Int.zero) { Type = RoomType.Start, Visited = true, Cleared = true };
        map[Vector2Int.zero] = start;

        var frontier = new List<RoomNode> { start };

        while (map.Count < targetRoomCount && frontier.Count > 0) {
            // pick a random existing room to branch from
            var current = frontier[rng.Next(frontier.Count)];

            var dirs = new List<Direction> { Direction.North, Direction.South, Direction.East, Direction.West };
            Shuffle(dirs, rng);

            bool placed = false;
            foreach (var dir in dirs) {
                var newPos = current.GridPos + DirectionUtil.ToOffset(dir);

                if (map.ContainsKey(newPos)) continue;
                if (CountNeighbors(map, newPos) > 1) continue; // avoid loops/clumping
                if (rng.NextDouble() < 0.5f) continue; // randomness in branching

                var newRoom = new RoomNode(newPos);
                map[newPos] = newRoom;
                frontier.Add(newRoom);

                current.Doors[dir] = true;
                newRoom.Doors[DirectionUtil.Opposite(dir)] = true;

                placed = true;
                break;
            }

            if (!placed) frontier.Remove(current); // dead end, stop trying from here
        }

        AssignSpecialRooms(map, rng);
        return map;
    }

    int CountNeighbors(Dictionary<Vector2Int, RoomNode> map, Vector2Int pos) {
        int count = 0;
        foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
            if (map.ContainsKey(pos + DirectionUtil.ToOffset(dir))) count++;
        return count;
    }

    void AssignSpecialRooms(Dictionary<Vector2Int, RoomNode> map, System.Random rng) {
        // Boss room = leaf room furthest from start
        RoomNode boss = null;
        int maxDist = -1;
        foreach (var kv in map) {
            if (kv.Value.Type != RoomType.Normal) continue;
            int neighborCount = CountNeighbors(map, kv.Key);
            if (neighborCount != 1) continue; // must be a leaf (dead end)

            int dist = Mathf.Abs(kv.Key.x) + Mathf.Abs(kv.Key.y);
            if (dist > maxDist) { maxDist = dist; boss = kv.Value; }
        }
        if (boss != null) boss.Type = RoomType.Boss;

        // Treasure room = another leaf, random
        var leaves = new List<RoomNode>();
        foreach (var kv in map)
            if (kv.Value.Type == RoomType.Normal && CountNeighbors(map, kv.Key) == 1)
                leaves.Add(kv.Value);

        if (leaves.Count > 0)
            leaves[rng.Next(leaves.Count)].Type = RoomType.Treasure;
    }

    void Shuffle<T>(List<T> list, System.Random rng) {
        for (int i = list.Count - 1; i > 0; i--) {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
        Dictionary<Vector2Int, RoomNode> debugMap;

    void Start() {
        debugMap = Generate();
    }

    void OnDrawGizmos() {
        if (debugMap == null) return;
        float roomSize = 2f;
        foreach (var kv in debugMap) {
            var pos = new Vector3(kv.Key.x, kv.Key.y) * roomSize;
            Gizmos.color = kv.Value.Type switch {
                RoomType.Start => Color.green,
                RoomType.Boss => Color.red,
                RoomType.Treasure => Color.yellow,
                _ => Color.white
            };
            Gizmos.DrawWireCube(pos, Vector3.one * roomSize * 0.9f);
        }
    }
}