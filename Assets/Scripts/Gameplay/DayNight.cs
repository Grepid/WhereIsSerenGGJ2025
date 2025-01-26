using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    private float timer = 0f;

    public float AmountOfDayTime;
    public float AmountOfNightTime;

    public Skybox_controls skybox;
    public LevelManager levelManager;

    private bool IsNightDay = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (IsNightDay)
        {
            if (timer >= AmountOfDayTime)
            {
                IsNightDay = false;
                skybox.ToggleDay();

                levelManager.started = false;

                timer = 0f;
            }
        }
        
        else
        {
            if (timer >= AmountOfNightTime)
            {
                IsNightDay = true;
                skybox.ToggleDay();

                levelManager.started = true;

                timer = 0f;
            }
        }
    }
}
