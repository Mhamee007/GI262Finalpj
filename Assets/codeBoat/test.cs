using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine.Rendering.Universal;

public class test : Equipment
{
    [SerializeField] Transform rodTip;
    [SerializeField] DistanceJoint2D fishingLine;
    [SerializeField] Rigidbody2D Rb2Dplayer;

    //Rod stats
    
     float chargeTime = 0f;
     float maxChargeTime = 1f;
     float baseThrowPower = 2f;

    bool canCast = true;
    bool bobberRetuen = false;
    public void SetRodStats(RodUpgradeSO.RodUpgradeData data)
    {
        this.maxLength = data.maxLength;
        this.reelSpeed = data.reelSpeed;
        this.throwPowerMultiplier = data.throwPowerMultiplier;

        Debug.Log($"Updread!: Ma={maxLength}, ReelSpeed={reelSpeed}");
    }


    void Update()
    {
        if (state == FishingState1.Idle)
        {
            bobber.linearDamping = 3;
            bobber.angularDamping = 5;
            if (canCast == true)
            {
                if (Input.GetMouseButton(0))
                {

                    chargeTime += Time.deltaTime;
                    chargeTime = Mathf.Min(chargeTime, maxChargeTime);
                    Debug.Log("Charg");
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    doCast();
                    Debug.Log("Cast");
                }
            }
            else
                Debug.Log("cantCast");
        }
        else if (state == FishingState1.Casted)
        {

            if (Input.GetMouseButton(0))
            {
                    ReelBobber();   
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezePositionY | 
                                         RigidbodyConstraints2D.FreezeRotation;
           
            }
        }
    }

    void doCast()
    {
      

        Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezeAll;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorld - rodTip.position).normalized;

        float clampedCharge = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        //upgrade
        float throwPower = clampedCharge * baseThrowPower * throwPowerMultiplier;
        bobber.transform.position = rodTip.position;
        bobber.linearVelocity = direction * throwPower;
        //upgrade
        float newLenghtLine = Mathf.Lerp(minLength, maxLength, clampedCharge / maxChargeTime);
        fishingLine.distance = newLenghtLine;

        canCast = false;

            state = FishingState1.Casted;
            chargeTime = 0f;
            Debug.Log($"Cast! Power={throwPower}");
            Debug.Log($"Cast! distant={newLenghtLine}");


    }

    void ReelBobber()
    {
         Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezeAll;
        state = FishingState1.Casted;

        //upgrade
        fishingLine.distance = Mathf.MoveTowards(
             fishingLine.distance,
              0.6f,
              reelSpeed * Time.deltaTime);

        Vector2 current = bobber.transform.position;
        Vector2 target = rodTip.transform.position;

        //upgrade
        float step = reelSpeed * Time.deltaTime;

        Vector2 next = Vector2.MoveTowards(current, target, step);
        bobber.transform.position = next;

        if (Vector2.Distance(current, target) <= minLength)
        {
            state = FishingState1.Idle;
            bobberRetuen = true;
            Debug.Log("Complete reeling (trigger)");
            StartCoroutine(DelayBeforeNextCast());
        }
        else
        {
            canCast = false;
            Debug.Log("reeling");
        }

    }

    IEnumerator DelayBeforeNextCast()
    {
        
        yield return new WaitForSeconds(3f); 
        canCast = true;
        Debug.Log("Ready to cast again!");
    }


    
}