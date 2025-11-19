using System;
using UnityEngine;

public enum FishingState1 { Idle, Charging, Casted, Reeling }

public class Equipment : MonoBehaviour
{
    public Rigidbody2D bobber;
    public float rodLvl;
    public float playerMoney;

    public FishingState1 state = FishingState1.Idle;

    public float reelSpeed = 0.1f;

    public float minLength = 3f;
    public float maxLength = 40f;

    public virtual void Description()
    {
        Console.WriteLine("Basic rod");
    }

    public virtual void money()
    {
        playerMoney = 100;
    }


}
