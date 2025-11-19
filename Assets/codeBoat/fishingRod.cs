using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class fishingRod : Equipment
{
    public Transform rodTip;   
    public Transform bobber;  
   [SerializeField] LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (rodTip == null || bobber == null) return;

        line.SetPosition(0, rodTip.position);
        line.SetPosition(1, bobber.position);
    }
}
