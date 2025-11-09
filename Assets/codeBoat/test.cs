using UnityEngine;
using System.Collections;


public enum FishingState1 { Idle, Charging, Casted, Reeling }

public class test : Equipment
{
    [SerializeField] Transform rodTip;
    [SerializeField] GameObject target; // target sprite
    [SerializeField] Rigidbody2D bobber;

    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float destroyAfter = 2f;

    private bool canShoot = true;

    //Rod
    public float chargeTime = 0f;
    public float maxChargeTime = 1f;

    public float reelSpeed = 3f;
    private FishingState1 state = FishingState1.Idle;
    private Vector2 targetPoint;
    private Vector2 velocity;

    void Update()
    {
        if (state == FishingState1.Idle)
        {
            if (Input.GetMouseButtonDown(0))
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
            ReelBobber();
            
        }

    }

    void doCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        Debug.Log("cast state");
        if (hit.collider != null)
        {


            Vector2 projectileVelocity = CalculateProjectileVelocity(rodTip.position, hit.point, chargeTime);
            target.transform.position = hit.point;
           
            bobber.AddForce(projectileVelocity * chargeTime, ForceMode2D.Impulse);
            bobber.transform.position = rodTip.position;
            bobber.linearVelocity = velocity;


            state = FishingState1.Casted;
            Debug.Log("Cast complete!");
        }

    }

    void ReelBobber()
    {
        state = FishingState1.Casted;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("reeling");
            bobber.transform.position = Vector2.MoveTowards(bobber.transform.position, rodTip.position, reelSpeed * Time.deltaTime);
            bobber.position = rodTip.position;
            state = FishingState1.Idle;

            Debug.Log("reel state");
        }



    }


    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }


}