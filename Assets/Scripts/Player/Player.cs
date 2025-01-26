using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Upgrades {ChargeSpeed,BubbleSize,PlayerSpeed,FireSpeed}

public class Player : MonoBehaviour
{
    public static float Points;
    public HUD hud;
    public static bool TryPurchase(float points)
    {
        if (Points - points < 0)
        {
            //play no money audio
            return false;
        }
        Points -= points;
        //Update some form of UI
        FishController.instance.playerStats.hud.UpdatePoints();
        return true;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Points = 0;
        FishController.instance.playerStats.hud.UpdatePoints();
    }

    public static void Upgrade(Upgrades type,float value)
    {
        switch (type)
        {
            case Upgrades.ChargeSpeed:
                BubbleLauncher.instance.TimeForMaxBubbleSize = NewValue(BubbleLauncher.instance.TimeForMaxBubbleSize, value);
                break;

            case Upgrades.BubbleSize:
                float increase = NewValue(BubbleLauncher.instance.BubbleSizeMinMax.y, value) - BubbleLauncher.instance.BubbleSizeMinMax.y;
                BubbleLauncher.instance.BubbleSizeMinMax.y += increase;
                BubbleLauncher.instance.BubbleSizeMinMax.x += (increase / 2);
                break;

            case Upgrades.PlayerSpeed:
                FishController.instance.moveSpeed = NewValue(FishController.instance.moveSpeed, value);
                break;

            case Upgrades.FireSpeed:
                BubbleLauncher.instance.ShotDelay = NewValue(BubbleLauncher.instance.ShotDelay, value);
                break;


        }
        AudioManager.Play("Purchase");
    }
    static float NewValue(float oldValue,float change)
    {
        float abs = (Mathf.Abs(change))/100;
        float slice = oldValue* abs;
        if(change < 0)
        {
            return oldValue - slice;
        }
        else
        {
            return oldValue + slice;
        }
    }
}
