using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum FishingState { Idle, Charging, Casted, Reeling }
public class fishingRod : Equipment
{

    //refabs

    public GameObject BaitPrefab;

     GameObject currentBait;
    
    Rigidbody2D bobberRb;
    public Transform rodTip;

    bait currentBaitScript;

    //Rod 
    public float chargeTime = 0f;
    public float maxChargeTime = 1f;

    //distance
    public float MinCastDistance = 5f;
    public float MaxCastDistance = 15f;

    //Speed
    public float reelSpeed = 0f;
    public float MinReelSpeed = 2f;
    public float MaxReelSpeed = 10f;

    float fishingPos;
    FishingState state = FishingState.Idle;
    Camera cam;


    private bool canShoot = true;
    [SerializeField] float spawnDelay = 5f;
    [SerializeField] float destroyAfter = 5f;

    void Start()
    {
        currentBait = Instantiate(BaitPrefab, rodTip.position, Quaternion.identity);
        bobberRb = currentBait.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        

        if (state == FishingState.Idle)
        {
            if (Input.GetMouseButton(0))
            {
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
                Debug.Log("Charge");
            }

            if (Input.GetMouseButtonUp(0))
            {
                DoCast();
                state = FishingState.Reeling;
                Debug.Log("Cast");
            }
        }

        if (state == FishingState.Reeling)
        {
            if (currentBait != null && currentBaitScript != null)
            {

                
            }
            else
            {    
                ResetFishing();
            }

        }

    }


    void DoCast()
    {
        
          

    }


    void ResetFishing()
    {
        state = FishingState.Idle;
        currentBait.transform.position = rodTip.position;
        chargeTime = 0f;
        Debug.Log("Reset");
    }

    


    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }
}
