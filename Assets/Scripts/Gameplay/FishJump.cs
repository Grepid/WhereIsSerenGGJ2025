using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum FishTypes {Blue,Red,Yellow,Green}
public struct FishInfo
{
    public FishTypes Type;
    public float ArcHeight;
    public float TravelTime;
    public float FishValue;

    public bool doesSpin;
    public FishInfo(FishTypes type,float arcHeight, float travelTime, float fishValue)
    {
        Type = type;
        ArcHeight = arcHeight;
        TravelTime = travelTime;
        FishValue = fishValue;
        doesSpin = false;
    }
}

public class FishJump : BubbleTarget
{
    public FishTypes type;
    public FishInfo info;
    public Vector3 startPoint;  // The starting point of the arc
    public Vector3 endPoint;    // The ending point of the arc
    public float arcHeight = 2;  // The maximum height of the arc
    public float travelTime = 2; // The time it takes to complete the arc
    public float Spinspeed = 60;

    private bool RunWaterOnce = true;
    public bool DoesSpin = false;
    public int FishValue;

    private float timer = 0f;     
    private bool isMoving = false;

    public GameObject SplashEffect;
    private AudioPlayer fishSoar;

    private void Update()
    {

        if (isMoving)
        {

            timer += Time.deltaTime;


            float progress = Mathf.Clamp01(timer / info.TravelTime);


            Vector3 start = startPoint;
            Vector3 end = endPoint;

            Vector3 horizontalPosition = Vector3.Lerp(start, end, progress);


            float height = Mathf.Sin(progress * Mathf.PI) * info.ArcHeight;

            transform.position = new Vector3(horizontalPosition.x, horizontalPosition.y + height, horizontalPosition.z);

            if (info.doesSpin == true)
            {
                transform.Rotate(Vector3.up, Spinspeed * Time.deltaTime, Space.Self);
            }

            if (progress >= 0.9f && RunWaterOnce == true)
            {
                var effect = Instantiate(SplashEffect);
                effect.transform.position = endPoint;
                RunWaterOnce = false;
            }

            if (progress >= 1f)
            {
                AudioManager.Play("FishDespawn",transform.position);
                if(fishSoar != null) fishSoar.Stop();
                Destroy(gameObject);
            }
        }
    }

    public void Start()
    {
        //Initialise(type, startPoint, endPoint);
        timer = 0f;
        isMoving = true;

        var effect = Instantiate(SplashEffect);
        AudioManager.Play("FishSpawn",transform.position);
        fishSoar = AudioManager.Play("FishSoar", gameObject, true);
        effect.transform.position = startPoint;
    }

    public void Initialise(FishTypes type,Vector3 start,Vector3 end)
    {
        info = FishType(type);
        startPoint = start;
        endPoint = end;
    }
    public static FishInfo FishType(FishTypes type)
    {
        FishInfo result = new FishInfo();

        switch (type)
        {
            case FishTypes.Blue:
                result = new FishInfo(FishTypes.Blue,5,1.5f,100);
                break;

            case FishTypes.Red:
                result = new FishInfo(FishTypes.Red, 5, 3, 50);
                break;

            case FishTypes.Yellow:
                result = new FishInfo(FishTypes.Yellow, 10, 2, 75);
                break;

            case FishTypes.Green:
                result = new FishInfo(FishTypes.Green, 7.5f, 2, 75);
                result.doesSpin = true;
                break;
        }
        
        return result;
    }

    public void CaughtFish()
    {
        if (fishSoar != null) fishSoar.Stop();
        Destroy(gameObject);
    }

}
