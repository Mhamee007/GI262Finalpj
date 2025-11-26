using NUnit.Framework.Internal;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public fishType fishData;

    public float speed = 2f;
    public float awarenessRadius = 5f; // รัศมีที่ปลาจะสนใจเหยื่อ
    public bool isHooked = false;

    [SerializeField] private Transform targetLure;
    private Vector2 randomTarget;
    private Rigidbody2D rb;

    public int moneyValue = 20;   
    public int expValue = 5;

    private float randomTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickRandomDirection();
    }

    void Update()
    {
        if (isHooked)
        {
            DetectLure();

            if (targetLure != null)
                MoveTowardLure();
            else
                SwimRandomly();
        }
        else
        {
            // แรงดิ้นตอนติดเบ็ด (ทำให้รู้สึกสมจริง)
            rb.velocity = new Vector2(
                Mathf.Sin(Time.time * 8f) * 0.5f,
                Mathf.Cos(Time.time * 6f) * 0.3f
            );
        }

        // 1. ตรวจสอบเหยื่อที่อยู่ใกล้
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, awarenessRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Lure"))
            {
                targetLure = col.transform;
                break;
            }
        }

        // 2. เคลื่อนที่ไปยังเป้าหมาย หรือว่ายน้ำแบบสุ่ม
        if (targetLure != null)
        {
            // ว่ายเข้าหาเหยื่อ (และมีโอกาสกัด)
            transform.position = Vector2.MoveTowards(transform.position, targetLure.position, speed * Time.deltaTime);
        }
    }

    void DetectLure()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, fishData.awarenessRadius);
        foreach (var col in cols)
        {
            if (col.CompareTag("Lure"))
            {
                if (Random.value < fishData.biteChance) // มีโอกาสไม่สนใจเหยื่อ
                    targetLure = col.transform;

                return;
            }
        }

        targetLure = null;
    }

    void MoveTowardLure()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetLure.position,
            fishData.speed * Time.deltaTime
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
            fishData.speed * 0.6f * Time.deltaTime
        );
    }

    void PickRandomDirection()
    {
        randomTarget = (Vector2)transform.position + Random.insideUnitCircle * 2f;
        randomTimer = Random.Range(1f, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bobber") && !isHooked)
        {
            isHooked = true;
            rb.velocity = Vector2.zero;
            other.GetComponent<rod>().AttachFish(this);
            Debug.Log($"Fish hooked: {fishData.fishName}");
        }
    }
    public void OnCaught()
    {
        Debug.Log($"Caught a {fishData.fishName}! +{fishData.price} coins");
        gameManager.instance.AddMoney(fishData.price);
        Destroy(gameObject);
    }
}