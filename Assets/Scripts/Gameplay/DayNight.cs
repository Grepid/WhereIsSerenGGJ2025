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
    public Light skylight;

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
                skylight.intensity = 0.5f;

                FishController.instance.PauseOxygen = true;

                levelManager.started = false;

                timer = 0f;
            }
        }
        
        else
        {
            if (timer >= AmountOfNightTime)
            {
                IsNightDay = true;
                FishController.instance.dayNNites++;
                skybox.ToggleDay();
                skylight.intensity = 1f;

                FishController.instance.PauseOxygen = false;

                levelManager.started = true;

                timer = 0f;
            }
        }
    }
}
