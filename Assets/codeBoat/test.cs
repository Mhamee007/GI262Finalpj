using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


 enum FishingState1 { Idle, Charging, Casted, Reeling }

public class test : Equipment
{
    [SerializeField] Transform rodTip;   
    [SerializeField] DistanceJoint2D fishingLine;

    //Rod
    [SerializeField] float chargeTime = 0f;
    [SerializeField] float maxChargeTime = 2f;

    public float reelSpeed = 0.1f;

    [SerializeField] float minLength = 3f;
    public float maxLength = 40f;

    FishingState1 state = FishingState1.Idle;

    

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
                ReelBobber();
            }
        }

    }

    void doCast()
    {

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorld - rodTip.position).normalized; 

        
        float clampedCharge = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        float throwPower = clampedCharge * 10f; 

         
        bobber.transform.position = rodTip.position;
        bobber.linearVelocity = direction * throwPower;

        float newLenghtLine = Mathf.Lerp(minLength, maxLength, clampedCharge / maxChargeTime);
        fishingLine.distance = newLenghtLine;


        chargeTime = 0f;
        state = FishingState1.Casted;
        Debug.Log($"Cast! Power={throwPower}");
        Debug.Log($"Cast! distant={newLenghtLine}");


    }

    void ReelBobber()
    {
            state = FishingState1.Casted;

            
            fishingLine.distance = 0.6f; 

            Vector2 current = bobber.transform.position;
            Vector2 target = rodTip.position; 

            float step = reelSpeed * Time.deltaTime;

            Vector2 next = Vector2.MoveTowards(current, target, step);
            bobber.transform.position = next;

        Debug.Log("reeling");


        if (Vector2.Distance(next, target) <= 0.05f)
            { 
                bobber.transform.position = rodTip.position;
                bobber.velocity = Vector2.zero;

                state = FishingState1.Idle;
                Debug.Log("Reel complete");
            }
            
           Debug.Log("reel state");
        

    }

    
}