using Unity.VisualScripting;
using UnityEngine;

public enum FishingState { Idle, Charging, Casted, Reeling, Returning }
public class fishingRod : Equipment
{

    //refabs
    public GameObject bobber;
    public Transform rodTip;
    public LineRenderer lineRenderer; 

    //Rod 
    public float chargeTime = 0f;
    public float maxChargeTime = 2f;

    public float MinCastDistance = 5f;
    public float MaxCastDistance = 15f;

    public float MinReelSpeed = 2f;
    public float MaxReelSpeed = 10f;

    FishingState state = FishingState.Idle;
    Camera cam;
    GameObject currentBobber;
    public void Awake()
    {
        cam = Camera.main;
        if (lineRenderer)
        {
            lineRenderer.positionCount = 3;
        }
    }

    void Start()
    {


    }
    void Update()
    {
        if (state == FishingState.Idle)
        {
            if (Input.GetMouseButton(0))
            {
                InCharge();
                Debug.Log("Charge");
            }
        }
        else if (state == FishingState.Charging)
        {
            if (Input.GetMouseButton(0))
            {
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
            }

            if (Input.GetMouseButtonUp(0))
            {
                DoCast();
                Debug.Log("Cast");
            }
        }
        else if (state == FishingState.Casted)
        {

            if (Input.GetMouseButton(0))
            {
                state = FishingState.Reeling;
                Debug.Log("Reeling");
            }
        }
    }

    void InCharge()
    {
        state = FishingState.Charging;
        chargeTime = 0f;
    }

    void DoCast()
    {
        float time = chargeTime / maxChargeTime;
        float force = Mathf.Lerp(MinCastDistance, MaxCastDistance, time);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector3 dir = (mouseWorld - rodTip.position).normalized;
        Vector2 castVelocity = dir * force;

        chargeTime = 0f;

    }
    void UpdateLineRenderer()
    {
        if (lineRenderer == null) return;

        if (state == FishingState.Casted || state == FishingState.Reeling || state == FishingState.Returning)
        {
            if (currentBobber != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, rodTip.position);
                lineRenderer.SetPosition(1, currentBobber.transform.position);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    
}
