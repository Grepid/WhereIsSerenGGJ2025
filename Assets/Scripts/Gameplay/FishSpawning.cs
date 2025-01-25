using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawning : MonoBehaviour
{
    private float timer = 0f;

    public GameObject Fish; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            Instantiate(Fish);

            timer = 0f;
        }
    }
}
