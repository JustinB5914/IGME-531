using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int gridX;
    public int gridY;
    public float angle;

    void Start()
    {
        transform.GetChild(1).rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        
    }

    Vector2 getGridPos()
    {
        return new Vector2(gridX, gridY);
    }

    float getAngle()
    {
        return angle % 360;
    }
}
