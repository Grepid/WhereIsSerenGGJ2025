using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy_menu : MonoBehaviour
{
    public float PlayersPoints;
    public float ChargeSpeedIncrease = 10;
    public float BubbleSizeAmount = 10;
    public float MoveSpeedIncrease = 25;

    public void ExitShop() 
    {
        this.gameObject.SetActive(false);
        FishController.instance.PlayerCursor(false);
        FishController.instance.acceptingInputs = true;
    }

    //Increases charge rate
    public void BuyCharge()
    {
       
    }

    //Increases bubbles size
    public void BuyBubble()
    {
        
    }

    //increase players movement
    public void BuySpeed()
    {
        //Does player have enough points

       

      //  FishController.instance.moveSpeed = FishController.instance.moveSpeed + MoveSpeedIncrease;
    }
}
