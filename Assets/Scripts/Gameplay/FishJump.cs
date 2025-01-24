using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : MonoBehaviour
{
    public Transform startPoint;  // The starting point of the arc
    public Transform endPoint;    // The ending point of the arc
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


            float progress = Mathf.Clamp01(timer / travelTime);


            Vector3 start = startPoint.position;
            Vector3 end = endPoint.position;

            Vector3 horizontalPosition = Vector3.Lerp(start, end, progress);


            float height = Mathf.Sin(progress * Mathf.PI) * arcHeight;

            transform.position = new Vector3(horizontalPosition.x, horizontalPosition.y + height, horizontalPosition.z);

            if (DoesSpin == true)
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
        timer = 0f;
        isMoving = true;
    }


    public void Fishtypes()
    {
        //Blue fish 
        arcHeight = 5;
        travelTime = 1.5f;
        FishValue = 100;


        //Red fish        
        arcHeight = 5;
        travelTime = 3;
        FishValue = 50;

        //Yellow fish
        arcHeight = 10;
        travelTime = 2;
        FishValue = 75;

        //Green fish
        arcHeight = 7.5f;
        travelTime = 2;
        FishValue = 75;
        DoesSpin = true;
    }

}
