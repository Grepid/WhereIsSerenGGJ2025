using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grepid.BetterRandom;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float spawnFrequency;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var pair = RandomSpawnPair();
        print(pair.Item1);
        print(pair.Item2);
    }

    public List<GameObject> FishSpawnClumps = new List<GameObject>();

    public Tuple<Transform,Transform> RandomSpawnPair()
    {
        Transform one, two;
        GameObject clump1, clump2;
        List<GameObject> clumps = Rand.RandFromCollection(FishSpawnClumps, 2, true).ToList();

        clump1 = clumps[0];
        clump2 = clumps[1];

        List<Transform> spawnPoints = new List<Transform>();

        foreach(Transform t in clump1.transform)
        {
            spawnPoints.Add(t);
        }
        one = Rand.RandFromCollection(spawnPoints);

        spawnPoints.Clear();

        foreach (Transform t in clump2.transform)
        {
            spawnPoints.Add(t);
        }
        two = Rand.RandFromCollection(spawnPoints);

        return Tuple.Create(one, two);
    }
}
