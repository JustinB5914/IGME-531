using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchotterGenerator : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private float gapMod;
    [SerializeField] private float deviation;
    [SerializeField] private GameObject squarePrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int w = 0; w < width; w++)
        {
            for(int h = 0; h < height; h++)
            {
                GameObject newSquare = Instantiate(squarePrefab);
                newSquare.transform.position = new Vector3((1 * gapMod * (w - width/2)) + (Random.Range(deviation/400 * (h-height), -deviation/400 * (h - height))),
                    1 * gapMod * (h - height/2) + (Random.Range(deviation / 400 * (h - height), -deviation / 400 * (h - height))),
                    0);
                newSquare.transform.Rotate(new Vector3(0,0, Random.Range(deviation * (h-height), -deviation * (h - height))));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
