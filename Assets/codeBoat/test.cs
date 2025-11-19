using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;

public class test : Equipment
{
    [SerializeField] Transform rodTip;
    [SerializeField] DistanceJoint2D fishingLine;
    public Rigidbody2D Rb2Dplayer;

    //Rod
    [SerializeField] float chargeTime = 0f;
    [SerializeField] float maxChargeTime = 2f;

    bool casted = false;
    bool reeled = false;

    void Start()
    {

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
                if (casted == true)
                {
                    Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezeAll;
                    ReelBobber();
                }
                else
                {
                    Debug.Log("Still cast...");
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezePositionY;
                Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezeRotation;
            
            }
        }

    }

    void doCast()
    {
        Rb2Dplayer.constraints = RigidbodyConstraints2D.FreezeAll;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorld - rodTip.position).normalized;


        float clampedCharge = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        float throwPower = clampedCharge * 10f;


        bobber.transform.position = rodTip.position;
        bobber.linearVelocity = direction * throwPower;

        float newLenghtLine = Mathf.Lerp(minLength, maxLength, clampedCharge / maxChargeTime);
        fishingLine.distance = newLenghtLine;

        if (true)
        {
            casted = true;
        }


        if (casted = true)
        {
            state = FishingState1.Casted;
            chargeTime = 0f;
            Debug.Log($"Cast! Power={throwPower}");
            Debug.Log($"Cast! distant={newLenghtLine}");
        }

    }

    void ReelBobber()
    {
        state = FishingState1.Casted;

            fishingLine.distance = Mathf.MoveTowards(
                 fishingLine.distance,
                  0.6f,
                  reelSpeed * Time.deltaTime );

        Vector2 current = bobber.transform.position;
        Vector2 target = rodTip.transform.position;
        float step = reelSpeed * Time.deltaTime;

        Vector2 next = Vector2.MoveTowards(current, target, step);
        bobber.transform.position = next;

        if (Vector2.Distance(current, target) < 0.1f)
        {
            state = FishingState1.Idle;
            Debug.Log("Complete reeling");
        }
        else
        {
            Debug.Log("reeling");
        }
            
    }

    public override void Description()
    {
        if (rodLvl >= 10)
        {
            Console.WriteLine("Normal rod");
        }
        
    }

}