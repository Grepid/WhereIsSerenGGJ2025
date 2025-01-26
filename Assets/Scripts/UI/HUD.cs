using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI points;

    public void UpdatePoints()
    {
        int rounded = Mathf.FloorToInt(Player.Points);
        points.text = rounded.ToString();
    }
}
