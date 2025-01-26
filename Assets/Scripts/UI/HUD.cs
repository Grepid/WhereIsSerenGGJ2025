using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI points;

    public void UpdatePoints()
    {
        int rounded = Mathf.RoundToInt(Player.Points - 0.5f);
        points.text = rounded.ToString();
    }
}
