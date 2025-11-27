using UnityEngine;

public class FishShyAI : FishManager
{

    [SerializeField] private Transform targetLure;
    Vector2 randomTarget;

    public bool isHooked = false;

    bool isFleeing = false;
    float fleeSpeedMultiplier;
    float fleeTimer = 0f;
    float fleeDuration = 1f;

    bool isResting = false;
    float restTimer = 0f;


    void Start()
    {
        fishRanSpeed = fishData.speed * Random.Range(0.7f, 1.5f);
        fleeSpeedMultiplier = Random.Range(1f, 2f);

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

        if (isResting)
        {
            restTimer -= Time.deltaTime;
            if (restTimer <= 0)
                isResting = false;
            return;
        }

        CheckFearAndFlee();


        if (isHooked)
        {
            isFleeing = false;
            isResting = false;
            FollowBobber();
            return;
        }
        else
            SwimRandomly();
            CheckFearAndFlee();

        if (isFleeing)
        {
            RunAwayBehavior();
            return;
        }

        MoveTowardLure();


    }
    void RunAwayBehavior()
    {
        fleeTimer -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(
            transform.position,
            randomTarget,
            fishRanSpeed * fleeSpeedMultiplier * Time.deltaTime
        );

        if (fleeTimer <= 0)
        {
            isFleeing = false;

            // เริ่มพัก 5 วิ
            isResting = true;
            restTimer = 5f;
        }
    }

    bool CheckFearAndFlee()
    {

        if (rodRef != null && rodRef.currentHookedFish != null && rodRef.currentHookedFish != this)
        {
            targetLure = null;

        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, fishData.awarenessRadius * 0.5f);

        foreach (var col in cols)
        {
            if (col.CompareTag("Bobber"))
            {
                StartFleeing(col.transform);
                return true;
            }
            Debug.Log("Run away");
        }

        return false;
    }

    void StartFleeing(Transform threat)
    {
        if (isHooked) return;

        isFleeing = true;
        fleeTimer = fleeDuration;

        Vector2 fleeDirection = (transform.position - threat.position).normalized;
        randomTarget = (Vector2)transform.position + fleeDirection * 3f;
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
        randomTimer = Random.Range(1f, 3f);
    }

    public void OnCaught()
    {
        gameManager.instance.AddMoney(fishData.price + 10);
        gameManager.instance.AddExp(fishData.exp + 1);

        if (rodRef != null && rodRef.currentHookedFishShy == this)
            rodRef.currentHookedFishShy = null;
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
            fishRanSpeed * 1.5f * Time.deltaTime
        );
    }
}
