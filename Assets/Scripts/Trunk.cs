using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public int InitialToSpawn = 5;
    public Transform Player;

    private List<GameObject> _spawned = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < InitialToSpawn; i++)
        {
            var spawned = SpawnRandom();
            var t = spawned.transform;
            if (_spawned.Count > 0)
            {
                var last = _spawned.Last().transform;
                var pos = last.position;
                var length = last.GetComponent<BoxCollider>().bounds.size.z;
                pos.z -= length - 0.15f;
                t.position = pos;
            }
            else
                t.position = transform.position;

            _spawned.Add(spawned);
            t.SetParent(transform);
        }
    }

    private GameObject SpawnRandom()
    {
        var random = Prefabs[Random.Range(0, Prefabs.Count - 1)];
        var spawned = Instantiate(random);
        return spawned;
    }
}
