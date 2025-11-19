using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Player : gameManager
{
    public float speed = 10f;
    public float exp;
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
