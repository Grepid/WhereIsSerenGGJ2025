using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishTypes {Blue,Red,Yellow,Green}

public class FishJump : MonoBehaviour
{
    public struct FishInfo
    {
        public float ArcHeight;
        public float TravelTime;
        public float FishValue;

        public bool doesSpin;
        public FishInfo(float arcHeight, float travelTime, float fishValue)
        {
            ArcHeight = arcHeight;
            TravelTime = travelTime;
            FishValue = fishValue;
            doesSpin = false;
        }
    }
    public FishTypes type;
    public FishInfo info;
    public Vector3 startPoint;  // The starting point of the arc
    public Vector3 endPoint;    // The ending point of the arc
    public float arcHeight = 2;  // The maximum height of the arc
    public float travelTime = 2; // The time it takes to complete the arc
    public float Spinspeed = 60;

    public bool DoesSpin = false;
    public int FishValue;

    private float timer = 0f;     // Tracks the time progress of the movement
    private bool isMoving = false;


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

            if (progress >= 1f)
            {
                timer = 0f;
                //isMoving = false;
            }
        }
    }

    public void Start()
    {
        Initialise(type,startPoint,endPoint);
        timer = 0f;
        isMoving = true;
    }


    public void Initialise(FishTypes type,Vector3 start,Vector3 end)
    {
        info = FishType(type);
        startPoint = start;
        endPoint = end;
    }
    public FishInfo FishType(FishTypes type)
    {
        FishInfo result = new FishInfo();

        switch (type)
        {
            case FishTypes.Blue:
                result = new FishInfo(5,1.5f,100);
                break;

            case FishTypes.Red:
                result = new FishInfo(5, 3, 50);
                break;

            case FishTypes.Yellow:
                result = new FishInfo(10, 2, 75);
                break;

            case FishTypes.Green:
                result = new FishInfo(7.5f, 2, 75);
                result.doesSpin = true;
                break;
        }
        
        return result;
    }

}
