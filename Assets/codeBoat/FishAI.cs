using NUnit.Framework.Internal;
using UnityEngine;

public class FishAI : FishManager
{

    [SerializeField] private Transform targetLure;
    Vector2 randomTarget;
    public rod rodRef;
    public bool isHooked = false;

    void Start()
    {
       
        if (rodRef == null)
        {
            rodRef = FindAnyObjectByType<rod>();
        }


        fishRanSpeed = fishData.speed * Random.Range(0.5f, 1f);
        PickRandomDirection();
       
    }

    void Update()
    {
        

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
            return;
        }


        if (!isHooked)
        {
            DetectLure();

            if (targetLure != null)
            {
                MoveTowardLure();
            }
            else
            {
                SwimRandomly();
            }

        }
        else
        {
            FollowBobber();
            return;
        }
    }

    void DetectLure()
    {
        if (rodRef != null && rodRef.currentHookedFish != null && rodRef.currentHookedFish != this)
        {
            targetLure = null;
            return;
        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, fishData.awarenessRadius);
        foreach (var col in cols)
        {
            if (col.CompareTag("Bobber"))
            {
                if (Random.value < fishData.biteChance) 
                {
                    targetLure = col.transform;
                    return;
                }
  
            }
        }

        targetLure = null;
    }

    void MoveTowardLure()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetLure.position,
            fishRanSpeed * Time.deltaTime
        );
    }

    void SwimRandomly()
    {
        randomTimer -= Time.deltaTime;
        if (randomTimer <= 0)
            PickRandomDirection();

        transform.position = Vector2.MoveTowards(
            transform.position,
            randomTarget,
            fishRanSpeed * 0.6f * Time.deltaTime
        );
    }

    void PickRandomDirection()
    {
        randomTarget = (Vector2)transform.position + Random.insideUnitCircle * 2f;
        randomTarget.y = Mathf.Clamp(randomTarget.y, -4, -23);
        randomTimer = Random.Range(1f, 3f);
    }

    public void OnCaught()
    {
        gameManager.instance.AddMoney(fishData.price);
        gameManager.instance.AddExp(fishData.exp);

        if (rodRef != null && rodRef.currentHookedFish == this)
            rodRef.currentHookedFish = null;
        isHooked = false;

        Destroy(gameObject);
    }

    void FollowBobber()
    {
        if (rodRef == null) return;

        Transform bobber = rodRef.bobber.transform;
        transform.position = Vector2.MoveTowards(
            transform.position,
            bobber.position,
            fishRanSpeed * 5f * Time.deltaTime
        );
    }
}