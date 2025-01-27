using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityValue : MonoBehaviour
{
    public TMP_InputField inputField;

    public void TryChangeSens()
    {
        string cleaned = inputField.text.Trim();
        if(float.TryParse(cleaned,out float result))
        {
            FishController.instance.mouseSens = result;
        }
        
    }
}
