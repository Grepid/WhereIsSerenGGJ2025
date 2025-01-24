using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BubbleTarget target = other.GetComponent<BubbleTarget>();
        if (target == null) return;
        target.OnHit();
    }
}
