using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalOrbit : MonoBehaviour
{
    void Update()
    {
        float CycleTime = 7.0f;
        float Height = 4.0f;
        float Width = 8.0f;
        float YOffset = 5.0f;
        transform.position = new Vector3(Mathf.Cos(Time.time / CycleTime) * Width, Mathf.Sin((Time.time + CycleTime) / CycleTime) * Height + YOffset, 0.0f);
    }
}
