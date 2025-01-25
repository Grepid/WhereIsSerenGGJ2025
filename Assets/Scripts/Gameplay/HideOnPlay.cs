using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPlay : MonoBehaviour
{
    private void Awake()
    {
        MeshRenderer render = GetComponent<MeshRenderer>();
        if (render == null) return;
        render.enabled = false;
    }
}
