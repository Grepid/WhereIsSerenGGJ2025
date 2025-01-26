using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Shop : MonoBehaviour
{
    public GameObject Shop_UI;

    private void Awake()
    {
        Shop_UI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != FishController.instance.gameObject) return;
        Shop_UI.gameObject.SetActive(true);
        print("entered");
        FishController.instance.PlayerCursor(true);
        FishController.instance.acceptingInputs = false;
        Buy_menu.InShop = true;
    }
}
