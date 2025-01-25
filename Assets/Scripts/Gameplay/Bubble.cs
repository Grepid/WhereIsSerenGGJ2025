using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    bool launched;
    Vector3 target;
    Vector3 direction;
    float speed;
    float launchTime;
    public float BubbleLifetime;
    public float onFishHitDistanceIncrease;
    bool hasHitFish;
    float popPitch;
    float speedMultiplier = 1;

    public List<FishInfo> fishList = new List<FishInfo>();



    private void OnTriggerEnter(Collider other)
    {
        BubbleTarget target = other.GetComponent<BubbleTarget>();
        if (target == null) return;
        target.OnHit();
        IncreaseDistance();
        FishJump fish = target.GetComponent<FishJump>();
        if (fish == null) return;
        hasHitFish = true;
        CaughtFish(fish);
        
    }

    public void IncreaseDistance()
    {
        target += (direction * onFishHitDistanceIncrease);
    }

    public void Launch(Vector3 direction, float distance, float speed,float popPitch)
    {
        this.popPitch = popPitch;
        this.target = Camera.main.transform.position + (direction * distance);
        this.direction = direction;
        this.speed = speed*speedMultiplier;
        transform.parent = null;
        launchTime = Time.time;
        launched = true;
    }
    private bool returning;

    private void Update()
    {
        if (launched)
        {
            if (returning)
            {
                speed += speed * 2f * Time.deltaTime;
            }
            Vector3 endGoal = returning ? FishController.instance.transform.position : target;
            transform.position = Vector3.MoveTowards(transform.position, endGoal, speed*Time.deltaTime);
            //if (Time.time >= launchTime+BubbleLifetime) EndBubble();
            if (Vector3.Distance(transform.position,endGoal) < 0.25f) CheckEnd();
        }
        
    }

    private void CaughtFish(FishJump fish)
    {
        GameObject fishObject = fish.gameObject;
        fishList.Add(FishJump.FishType(fish.type));
        fish.enabled = false;
        Collider col = fishObject.GetComponent<Collider>();
        col.enabled = false;
        fishObject.transform.parent = transform;
        fishObject.transform.localPosition = Vector3.zero;
        
    }

    public void CheckEnd()
    {
        if (!returning)
        {
            if (!hasHitFish)
            {

                DestroyBubble();
            }
            returning = true;
            return;
        }
        if (returning)
        {
            DestroyBubble();
        }
    }
    public void DestroyBubble()
    {
        var source = AudioManager.Play("BubblePop", transform.position);
        source.AudioSource.pitch = popPitch;

        if(fishList.Count > 0)
        {
            foreach(FishInfo f in fishList)
            {
                print(f.Type);
            }
        }

        Destroy(gameObject);
    }
}
