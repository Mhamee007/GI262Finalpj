using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float speed = 2f;
    public float awarenessRadius = 5f; // รัศมีที่ปลาจะสนใจเหยื่อ
    public bool isHooked = false;

    private Transform targetLure; // ตำแหน่งเหยื่อ

    void Update()
    {
        if (isHooked)
        {
            //  (ดึง, สู้แรงตึง)
            return;
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
        else
        {
            // ว่ายแบบสุ่ม 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bobber") && !isHooked)
        {
            // ปลาชนเหยื่อ => กัด
            isHooked = true;
            other.GetComponent<fishingRod>().AttachFish(this);

        }
    }
}