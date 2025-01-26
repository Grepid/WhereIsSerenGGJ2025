using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_menu : MonoBehaviour
{
    public static bool InShop;
    public float ChargeSpeedIncrease = 10;
    public float BubbleSizeAmount = 10;
    public float MoveSpeedIncrease = 25;

    public float ChargeSpeedCost,BubbleSizeCost,MoveSpeedCost;

    public void ExitShop() 
    {
        
        this.gameObject.SetActive(false);
        FishController.instance.PlayerCursor(false);
        FishController.instance.acceptingInputs = true;
        Invoke("ReAllowBubbles", 0.25f);
    }
    private void ReAllowBubbles()
    {
        InShop = false;
    }
    //Increases charge rate
    public void BuyCharge()
    {
        if (!Player.TryPurchase(ChargeSpeedCost)) return;
        Player.Upgrade(Upgrades.ChargeSpeed, ChargeSpeedIncrease);
    }

    //Increases bubbles size
    public void BuyBubble()
    {
        if (!Player.TryPurchase(BubbleSizeCost)) return;
        Player.Upgrade(Upgrades.BubbleSize, BubbleSizeAmount);
    }

    //increase players movement
    public void BuySpeed()
    {
        //Does player have enough points

        if (!Player.TryPurchase(MoveSpeedCost)) return;
        Player.Upgrade(Upgrades.PlayerSpeed, MoveSpeedIncrease);

        //  FishController.instance.moveSpeed = FishController.instance.moveSpeed + MoveSpeedIncrease;
    }
}
