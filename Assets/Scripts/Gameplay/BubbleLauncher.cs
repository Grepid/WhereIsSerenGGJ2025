using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLauncher : MonoBehaviour
{
    public Vector2 BubbleSizeMinMax;
    public Vector2 BubbleSpeedMinMax;
    public Vector2 BubblePopPitchMinMax;
    public float TimeForMaxBubbleSize;
    public Transform BubbleSpawn;
    public GameObject BubblePrefab;
    private Bubble spawnedBubble;
    private float sizeAlpha;
    public float initialBubbleDistance;
    float lastShotTime;
    public float ShotDelay;

    public static BubbleLauncher instance;

    private void Awake()
    {
        instance = this;
    }
    float maxHeldTime;
    private void Update()
    {
        CheckInputs();
        if(spawnedBubble != null)
        {
            if(sizeAlpha >= 1)
            {
                maxHeldTime += Time.deltaTime;
                if(maxHeldTime > 1) PopBubble();
                return;
            }
            spawnedBubble.transform.localScale = Vector3.one * Mathf.Lerp(BubbleSizeMinMax.x, BubbleSizeMinMax.y, sizeAlpha);
            sizeAlpha += (Time.deltaTime / TimeForMaxBubbleSize);
            //spawnedBubble.transform.localScale = Vector3.one * Mathf.Clamp(spawnedBubble.transform.localScale.x + ,BubbleSizeMinMax)
        }
    }
    private void CheckInputs()
    {
        if (Buy_menu.InShop) return;
        if (Input.GetMouseButton(0))
        {
            if (Time.time < lastShotTime + ShotDelay) return;
            if (spawnedBubble != null) return;
            SpawnBubble();
        }
        if (Input.GetMouseButtonUp(0))
        {
            FireBubble();
        }
    }
    private void SpawnBubble()
    {
        var bubbleObj = Instantiate(BubblePrefab, BubbleSpawn);
        spawnedBubble = bubbleObj.GetComponent<Bubble>();
        spawnedBubble.transform.localPosition = Vector3.zero;
        //spawnedBubble.transform.position = BubbleSpawn.TransformPoint(Vector3.zero);
        spawnedBubble.transform.localScale = Vector3.zero;
    }
    private void FireBubble()
    {
        if (spawnedBubble == null) return;
        //Vector3 targetPoint = Camera.main.transform.position + Camera.main.transform.forward*initialBubbleDistance;
        float pitch = Mathf.Lerp(BubblePopPitchMinMax.y, BubblePopPitchMinMax.x, sizeAlpha);
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 4;
        float speed = Mathf.Lerp(BubbleSpeedMinMax.y, BubbleSizeMinMax.x, sizeAlpha);
        spawnedBubble.Launch(Camera.main.transform.forward, initialBubbleDistance, speed,pitch);
        spawnedBubble.transform.position = spawnPos;
        spawnedBubble = null;
        lastShotTime = Time.time;
        sizeAlpha = 0;
    }
    private void PopBubble()
    {
        var source = AudioManager.Play("BubblePop", transform.position);
        source.AudioSource.pitch = Mathf.Lerp(BubblePopPitchMinMax.y, BubblePopPitchMinMax.x, sizeAlpha);
        maxHeldTime = 0;
        Destroy(spawnedBubble.gameObject);
        sizeAlpha = 0;
    }


}
