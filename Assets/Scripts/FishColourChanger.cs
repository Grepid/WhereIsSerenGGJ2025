using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishColourChanger : MonoBehaviour
{
    public FishJump fish;

    public SkinnedMeshRenderer SMrenderer;

    public Material blue, red, yellow, green;

    public void SetColour(FishTypes type)
    {
        switch (type)
        {
            case FishTypes.Blue:
                SMrenderer.material = blue;
                break;

            case FishTypes.Red:
                SMrenderer.material = red;
                break;

            case FishTypes.Yellow:
                SMrenderer.material = yellow;
                break;

            case FishTypes.Green:
                SMrenderer.material = green;
                break;
        }
    }
}
