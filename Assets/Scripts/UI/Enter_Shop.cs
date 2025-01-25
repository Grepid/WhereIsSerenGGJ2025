using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Shop : MonoBehaviour
{
    public GameObject Shop_UI;

    private void OnTriggerEnter(Collider other)
    {
        Shop_UI.gameObject.SetActive(true);

        FishController.instance.PlayerCursor(true);
        FishController.instance.acceptingInputs = false;
    }
}
