using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : MonoBehaviour
{
    public Transform startPoint;  // The starting point of the arc
    public Transform endPoint;    // The ending point of the arc
    public float arcHeight = 2f;  // The maximum height of the arc
    public float travelTime = 2f; // The time it takes to complete the arc

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

            if (progress >= 1f)
            {
                isMoving = false;
            }
        }
    }

    public void Start()
    {
        timer = 0f;
        isMoving = true;
    }
}
