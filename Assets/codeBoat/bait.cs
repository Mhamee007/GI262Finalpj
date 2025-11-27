using System.Reflection;
using UnityEngine;

public class bait : Equipment
{

    public rod rodRef;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            bobber.linearDamping = 5;
            bobber.angularDamping = 8;
            Debug.Log("enter water");
        }

        FishAI fish = collision.GetComponent<FishAI>();
        FishShyAI fishShy = collision.GetComponent<FishShyAI>();

        if (fish != null && !fish.isHooked)
        {
            if (rodRef.currentHookedFish != null)
                return;

           
            if (!fish.isHooked)
            {
                rodRef.AttachFish(fish);
            }
        }

        if (fishShy != null && !fishShy.isHooked)
        {
            if (rodRef.currentHookedFishShy != null)
                return;

            rodRef.AttachFishShy(fishShy);

            fishShy.isHooked = true;     
        }



    }

    private void OnTriggerExit2D(Collider2D outWater)
    {
        if (outWater.CompareTag("Water"))
        {
            bobber.linearDamping = 3;
            bobber.angularDamping = 5;
            Debug.Log("enter water");
        }
    }

  
}


