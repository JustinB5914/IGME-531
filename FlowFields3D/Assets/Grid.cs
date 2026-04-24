using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grid : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject linePrefab;
    public GameObject leavesPrefab;
    public int gridX;
    public int gridZ;
    public float pointDistance;
    public int stepCount;
    public int branchCount;
    public int branchStepCount;
    public GameObject[,] grid;
    public List<GameObject> branches = new List<GameObject>();
    void Start()
    {
        grid = new GameObject[gridX, gridZ];
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridZ; j++)
            {
                GameObject point = Instantiate(pointPrefab);
                point.GetComponent<PointScript>().gridX = i;
                point.GetComponent<PointScript>().gridY = j;
                point.GetComponent<PointScript>().angle = (Mathf.PerlinNoise(i * 0.1f, j * 0.1f) - 0.5f) * 60f;
                point.transform.position = new Vector3(-((float)gridX / 2f * pointDistance) + i * pointDistance, 0f, -((float)gridZ / 2f * pointDistance) + j * pointDistance);
                point.transform.parent = gameObject.transform;
                grid[i, j] = point;
            }
        }
        //draw the trunk
        Vector3[] trunkPoints = DrawCurve(stepCount, new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
        for (int i = 0; i < branchCount; i++)
        {
            int branchStep = Random.Range(stepCount / 4, stepCount - 2);
            Vector3 branchOrigin = trunkPoints[branchStep];
            //perlin noise offset for each branch so they go in a different direciton then the trunk
            float sideOffset = Random.Range(-50f, 50f);
            DrawBranch(branchStepCount, new Vector2(branchOrigin.x, branchOrigin.z), branchOrigin.y, sideOffset);
        }
        // stick leaves at the end of every branch
        foreach (GameObject branch in branches)
        {
            GameObject leaves = Instantiate(leavesPrefab);
            leaves.transform.position = branch.GetComponent<LineRenderer>().GetPosition(branch.GetComponent<LineRenderer>().positionCount - 1);
        }
    }
    Vector3[] DrawCurve(int stepCount, Vector2 startPoint)
    {
        //creates a new line
        GameObject line = Instantiate(linePrefab);
        //adds it to the branch list so i can add leaves later
        branches.Add(line);
        line.GetComponent<LineRenderer>().positionCount = stepCount;
        Vector3[] linePoints = new Vector3[stepCount];
        linePoints[0] = new Vector3(startPoint.x, 0f, startPoint.y);
        //for each step of the branch
        for (int i = 1; i < stepCount; i++)
        {
            float x = linePoints[i - 1].x;
            float z = linePoints[i - 1].z;
            float newX = (Mathf.PerlinNoise(x * 0.1f, z * 0.1f) - 0.5f) * 2f;
            float newZ = (Mathf.PerlinNoise(x * 0.1f + 100f, z * 0.1f + 100f) - 0.5f) * 2f;
            linePoints[i] = new Vector3(x + newX, linePoints[i - 1].y + 0.25f, z + newZ);
        }
        line.GetComponent<LineRenderer>().SetPositions(linePoints);
        return linePoints;
    }
    void DrawBranch(int stepCount, Vector2 startPoint, float startY, float sideOffset)
    {
        //creates a new line for the branch
        GameObject line = Instantiate(linePrefab);
        branches.Add(line);
        line.GetComponent<LineRenderer>().positionCount = stepCount;
        Vector3[] linePoints = new Vector3[stepCount];
        //start on a point on the trunk
        linePoints[0] = new Vector3(startPoint.x, startY, startPoint.y);
        Debug.Log(linePoints[0]);

        for (int i = 1; i < stepCount; i++)
        {
            float x = linePoints[i - 1].x;
            float z = linePoints[i - 1].z;
            //sideOffset shifts the perlin noise used so the branch doesnt follow the trunk
            float newX = (Mathf.PerlinNoise((x + sideOffset) * 0.1f, (z + sideOffset) * 0.1f) - 0.5f) * 2f;
            float newZ = (Mathf.PerlinNoise((x + sideOffset) * 0.1f + 100f, (z + sideOffset) * 0.1f + 100f) - 0.5f) * 2f;

            //Debug.Log(newX + ", " + newZ);

            linePoints[i] = new Vector3(x + newX, linePoints[i - 1].y + 0.15f, z + newZ);
        }
        line.GetComponent<LineRenderer>().SetPositions(linePoints);
    }
}