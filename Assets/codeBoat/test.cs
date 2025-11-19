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
    float minLength = 3f;
     float chargeTime = 0f;
     float maxChargeTime = 2f;
     float baseThrowPower = 2f;

    bool canCast = true;
    bool bobberRetuen = false;
    void Awake()
    {
        ApplyUpgradeStats();
    }


    void Update()
    {
        if (state == FishingState1.Idle)
        {
            bobber.linearDamping = 3;
            bobber.angularDamping = 5;

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
        int index = Mathf.Clamp(currentLevel, 0, upgrades.Length - 1);

        Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezeAll;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorld - rodTip.position).normalized;


        if (upgrades == null || upgrades.Length == 0)
        {
            Debug.LogError("Upgrades array is EMPTY! Add elements in Inspector.");
            return;
        }

        if (currentLevel >= upgrades.Length)
        {
            Debug.LogError("CurrentLevel is higher than upgrades size!");
            return;
        }

        float clampedCharge = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        float throwPower = clampedCharge * baseThrowPower * throwPowerMultiplier;
        bobber.transform.position = rodTip.position;
        bobber.linearVelocity = direction * throwPower;

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

        fishingLine.distance = Mathf.MoveTowards(
             fishingLine.distance,
              0.6f,
              reelSpeed * Time.deltaTime);

        Vector2 current = bobber.transform.position;
        Vector2 target = rodTip.transform.position;
        float step = reelSpeed * Time.deltaTime;

        Vector2 next = Vector2.MoveTowards(current, target, step);
        bobber.transform.position = next;

        if (Vector2.Distance(current, target) < 0.1f)
        {
            state = FishingState1.Idle;
            bobberRetuen = true;
            Debug.Log("Complete reeling");
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
        canCast = false;

        yield return new WaitForSeconds(1f); // ดีเลย์ 1 วินาที

        canCast = true;
        Debug.Log("Ready to cast again!");
    }

    

}