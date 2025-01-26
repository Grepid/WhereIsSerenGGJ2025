using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    private float timer = 0f;

    public float AmountOfDayTime;

    

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= AmountOfDayTime)
        {
            

            timer = 0f;
        }
    }
}
