using UnityEngine;

public class EndBGSlide : MonoBehaviour
{
    public float moveSpeed = 2f;       // ความเร็ว
    public Vector3 endPosition;        // จุดที่ต้องการให้ BG ไปถึง
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            endPosition,
            moveSpeed * Time.deltaTime
        );
    }
}

