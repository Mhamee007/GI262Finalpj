using UnityEngine;

public class cameramove : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform target;
    public float followSpeed = 5f;  
    public Vector3 offset;         

    private Transform currentFollow; 

    void Start()
    {
        currentFollow = player;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentFollow = target;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentFollow = player;
        }
    }

    void LateUpdate()
    {
        if (currentFollow == null) return;

        Vector3 desiredPosition = currentFollow.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
        
}
