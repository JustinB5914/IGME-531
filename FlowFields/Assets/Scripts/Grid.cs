using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject linePrefab;
    [SerializeField] private int gridX;
    [SerializeField] private int gridY;
    [SerializeField] private float pointDistance;
    private GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[gridX, gridY];
        for (int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                GameObject point = Instantiate(pointPrefab);
                point.GetComponent<Point>().gridX = i;
                point.GetComponent<Point>().gridY = j;
                point.GetComponent<Point>().angle = Mathf.PerlinNoise(i * 0.1f,j * 0.1f) * 360;

                point.transform.position = new Vector3(-((float)gridX / 2 * pointDistance) + i * pointDistance, -((float)gridY / 2 * pointDistance) + j * pointDistance, 0);

                point.transform.parent = gameObject.transform;

                grid[i, j] = point;
            }
        }

        for(int i = 0;i < 75;i++)
        {
            DrawCurve(60, new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-4.0f, 4.0f)));
        }
    }

    void Update()
    {
        
    }

    void DrawCurve(int stepCount, Vector2 startPoint)
    {
        GameObject line = Instantiate(linePrefab);
        line.transform.position = Vector2.zero;
        line.GetComponent<LineRenderer>().positionCount = stepCount;
        Vector3[] linePoints = new Vector3[stepCount];
        linePoints[0] = startPoint;
        GameObject oldPoint = FindNearestPoint(startPoint);
        GameObject newPoint;

        for (int i = 1; i < stepCount; i++)
        {
            newPoint = FindNearestPoint(linePoints[i-1] + (Vector3)UnitVectorFromAngle(oldPoint.GetComponent<Point>().angle) * pointDistance);

            linePoints[i] = linePoints[i-1] + (Vector3)UnitVectorFromAngle(newPoint.GetComponent<Point>().angle) * pointDistance;

            oldPoint = FindNearestPoint(linePoints[i]);
        }

        Debug.Log(linePoints);
        line.GetComponent<LineRenderer>().SetPositions(linePoints);
    }

    GameObject FindNearestPoint(Vector2 pos)
    {
        float originX = -(gridX - 1) * pointDistance / 2f;
        float originY = -(gridY - 1) * pointDistance / 2f;

        float localX = pos.x - originX;
        float localY = pos.y - originY;

        int i = Mathf.RoundToInt(localX / pointDistance);
        int j = Mathf.RoundToInt(localY / pointDistance);

        i = Mathf.Clamp(i, 0, gridX - 1);
        j = Mathf.Clamp(j, 0, gridY - 1);

        return grid[i, j].gameObject;
    }

    Vector2 UnitVectorFromAngle(float angle)
    {
        Debug.Log("Vector from angle " + angle + " = " + new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle)));
        return new Vector2(-Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
    }
}
