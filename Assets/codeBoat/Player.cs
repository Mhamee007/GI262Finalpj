using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rd;

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float movement = Input.GetAxisRaw("Horizontal");
        rd.linearVelocity = new Vector2(movement * speed, rd.linearVelocityY);
    }
}
