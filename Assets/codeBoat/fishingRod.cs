using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class fishingRod : Equipment
{
    public Transform rodTip;   // จุดปลายคันเบ็ด
    public Transform bobber;   // เหยื่อ

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
