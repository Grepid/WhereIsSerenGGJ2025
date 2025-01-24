using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLauncher : MonoBehaviour
{
    public Vector2 BubbleSizeMinMax;
    public Vector2 BubbleSpeedMinMax;
    public float TimeForMaxBubbleSize;
    public Transform BubbleSpawn;
    public GameObject BubblePrefab;
    private Bubble spawnedBubble;
    private float sizeAlpha;


    private void Update()
    {
        CheckInputs();
        if(spawnedBubble != null)
        {
            if(sizeAlpha >= 1)
            {
                PopBubble();
                return;
            }
            spawnedBubble.transform.localScale = Vector3.one * Mathf.Lerp(BubbleSizeMinMax.x, BubbleSizeMinMax.y, sizeAlpha);
            sizeAlpha += (Time.deltaTime / TimeForMaxBubbleSize);
            //spawnedBubble.transform.localScale = Vector3.one * Mathf.Clamp(spawnedBubble.transform.localScale.x + ,BubbleSizeMinMax)
        }
    }
    private void CheckInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        Vector3 targetPoint = Camera.main.transform.position + Camera.main.transform.forward*1000;
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 4;
        //Vector3 direction = 
        spawnedBubble.Launch(targetPoint, Mathf.Lerp(BubbleSpeedMinMax.y, BubbleSizeMinMax.x, sizeAlpha));
        spawnedBubble.transform.position = spawnPos;
        spawnedBubble = null;
        sizeAlpha = 0;
    }
    private void PopBubble()
    {
        Destroy(spawnedBubble.gameObject);
        sizeAlpha = 0;
    }
}
