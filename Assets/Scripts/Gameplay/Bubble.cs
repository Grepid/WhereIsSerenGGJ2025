using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    bool launched;
    Vector3 target;
    float speed;
    float launchTime;
    public float BubbleLifetime;
    private void OnTriggerEnter(Collider other)
    {
        BubbleTarget target = other.GetComponent<BubbleTarget>();
        if (target == null) return;
        target.OnHit();
    }
    public void Launch(Vector3 target, float speed)
    {
        this.target = target;
        this.speed = speed;
        transform.parent = null;
        launchTime = Time.time;
        launched = true;
    }

    private void Update()
    {
        if (launched)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
            if(Time.time >= launchTime+BubbleLifetime) EndBubble();
        }
    }

    public void EndBubble()
    {
        Destroy(gameObject);
    }
}
