using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grepid.BetterRandom;
using System.Linq;
using AudioSystem;

// Add Delay of spawning with a particle showing where it will spawn


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject FishPrefab;

    public GameObject fishSpawningParticle;

    [SerializeField]
    private List<FishSpawnChance> FishSpawnChances;

    public Vector2 SpawnTimeMinMax;
    public float GetSpawnChance(FishTypes type)
    {
        foreach(FishSpawnChance chance in FishSpawnChances)
        {
            if(chance.type == type) return chance.chance;
        }
        return 0;
    }

    public float SecondsBetweenSpawnSpeedIncreases,SecondsQuickerPerDecrease;
    private float timeOfLastSpeedIncrease;
    public float MinimumSpawnTime;
    private float spawnFrequency;

    bool started;

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
        //Invoke("SpawnFish",0.1f);
        //SpawnFish();
        started = true;
    }

    float lastSpawnTime;

    private void Update()
    {
        if (started)
        {
            if(Time.time > timeOfLastSpeedIncrease + SecondsBetweenSpawnSpeedIncreases)
            {
                SpawnTimeMinMax.x = Mathf.Clamp(SpawnTimeMinMax.x - SecondsQuickerPerDecrease, MinimumSpawnTime, 999);
                SpawnTimeMinMax.y = Mathf.Clamp(SpawnTimeMinMax.y - (SecondsQuickerPerDecrease*0.75f), MinimumSpawnTime, 999);
                timeOfLastSpeedIncrease = Time.time;
            }
            if(Time.time > lastSpawnTime + spawnFrequency)
            {
                SpawnFish();
                lastSpawnTime = Time.time;
            }
        }
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

        GameObject fishObj = Instantiate(FishPrefab,points.Item1.position,Quaternion.identity);
        FishJump fish = fishObj.GetComponent<FishJump>();
        //print(type);
        fish.Initialise(type, points.Item1.position, points.Item2.position);
        fish.gameObject.SetActive(false);
        StartCoroutine(StartSpawnDelay(fish));
        spawnFrequency = UnityEngine.Random.Range(SpawnTimeMinMax.x, SpawnTimeMinMax.y);
    }
    public float SpawnDelay;
    public IEnumerator StartSpawnDelay(FishJump fish)
    {
        AudioManager.Play("FishTelograph",fish.startPoint);
        GameObject particle = Instantiate(fishSpawningParticle);
        particle.transform.position = fish.startPoint;
        yield return new WaitForSeconds(SpawnDelay);
        Destroy(particle);
        fish.gameObject.SetActive(true);
    }
}
