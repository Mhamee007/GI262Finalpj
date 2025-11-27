using System;
using UnityEngine;

public enum FishingState1 { Idle, Charging, Casted, Reeling }

public class Equipment : MonoBehaviour
{
    public Rigidbody2D bobber;

    protected float throwPowerMultiplier = 1f;
    protected float reelSpeed = 0.1f;

    protected float minLength = 1f;
    protected float maxLength = 50f;


    public FishingState1 state = FishingState1.Idle;
}


