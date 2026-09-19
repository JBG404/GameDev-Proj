using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Roguelike/Room Database")]
public class RoomDatabase : ScriptableObject
{
    [System.Serializable]
    public class Entry
    {
        public RoomType type;
        public GameObject prefab;
    }

    public List<Entry> rooms;

    public GameObject GetRandomPrefab(RoomType type, System.Random rng)
    {
        var matches = rooms.Where(r => r.type == type).ToList();
        if (matches.Count == 0) return null;
        return matches[rng.Next(matches.Count)].prefab;
    }
}
