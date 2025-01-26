using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox_controls : MonoBehaviour
{
    public float skySpeed;

    public Material Day;
    public Material Night;

    private bool flipflop = true;


    private void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }

    public void ToggleDay()
    {
        if (flipflop == true)
        {
            flipflop = !flipflop;
            RenderSettings.skybox = Night;
          
        }

        else
        {
            flipflop = !flipflop;
            RenderSettings.skybox = Day;
        }
    }
}
