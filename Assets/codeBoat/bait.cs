using UnityEngine;

public class bait : MonoBehaviour
{
    public float waterDrag = 3f;
    public float airDrag = 0f;
    public float floatDamping = 0.5f;

    public bool inWater = false;
    public bool hasBite = false;

    public float biteDelayMin = 1.5f;
    public float biteDelayMax = 6f;

    Rigidbody2D rb;
    Collider2D col;
    float biteTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        // Initially no drag in air
        rb.drag = airDrag;
    }

    void Update()
    {
        if (inWater && !hasBite)
        {
            biteTimer -= Time.deltaTime;
            if (biteTimer <= 0f)
            {
                // simple random chance to bite
                hasBite = true;
                // you can spawn visual effect or notify player
                Debug.Log("A fish is biting!");
            }
        }

        // optional minor float damping for nicer motion:
        if (inWater)
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, floatDamping * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If hits water layer, start floating
        // Expect user to set water collider(s) on waterMask or use tag "Water"
        if (collision.collider.CompareTag("Water") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            EnterWater();
        }
    }

    void EnterWater()
    {
        if (inWater) return;
        inWater = true;
        rb.velocity *= 0.2f;
        rb.drag = waterDrag;
        // start bite timer
        biteTimer = Random.Range(biteDelayMin, biteDelayMax);
    }

    // Called when player collects/bobbber returned
    public void OnCollected()
    {
        if (hasBite)
        {
            // you caught something! spawn item, give reward...
            Debug.Log("You caught a fish!");
        }
        else
        {
            Debug.Log("No catch this time.");
        }
    }
}


