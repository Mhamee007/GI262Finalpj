using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 20f;
    public float maxSpeed = 10f;
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

    private void FixedUpdate()
    {
       
    }
}
