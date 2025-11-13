using UnityEngine;

public enum FishingState1 { Idle, Charging, Casted, Reeling }

public class Equipment : MonoBehaviour
{
    public Rigidbody2D bobber;
    string name;
    string description;
    float price;

    public FishingState1 state = FishingState1.Idle;

    public float reelSpeed = 0.1f;

    public float minLength = 3f;
    public float maxLength = 40f;
}
