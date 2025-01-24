using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTarget : MonoBehaviour
{
    public void OnHit()
    {
        Destroy(gameObject);
    }
}
