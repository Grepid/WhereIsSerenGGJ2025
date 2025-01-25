using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grepid.BetterRandom;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject FishPrefab;

    [SerializeField]
    private List<FishSpawnChance> FishSpawnChances;
    public float GetSpawnChance(FishTypes type)
    {
        foreach(FishSpawnChance chance in FishSpawnChances)
        {
            if(chance.type == type) return chance.chance;
        }
        return 0;
    }

    public float spawnFrequency;

    [System.Serializable]
    public struct FishSpawnChance
    {
        public FishTypes type;
        public float chance;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnFish();
    }

    private void DebugPrints()
    {
        var pair = RandomSpawnPair();
        print(pair.Item1);
        print(pair.Item2);
        var type = RandomType();
        print(type.ToString());
    }

    public List<GameObject> FishSpawnClumps = new List<GameObject>();

    public FishTypes RandomType()
    {

        List<float> chances = new List<float>();
        foreach(FishSpawnChance c in FishSpawnChances)
        {
            chances.Add(c.chance);
        }
        int index = Weighted.RandomIndex(chances);

        return FishSpawnChances[index].type;
    }
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

    public void SpawnFish()
    {
        var points = RandomSpawnPair();
        var type = RandomType();

        GameObject fishObj = Instantiate(FishPrefab);
        FishJump fish = fishObj.GetComponent<FishJump>();
        fish.Initialise(type, points.Item1.position, points.Item2.position);
    }
}
