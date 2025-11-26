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

        if (fish != null && !fish.isHooked)
        {
            fish.isHooked = true;
            rodRef.AttachFish(fish);
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


