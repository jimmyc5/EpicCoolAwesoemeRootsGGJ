using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootSpawner : MonoBehaviour
{
    public List<Vector3> points;
    Vector3[] linePoints;
    private LineRenderer lr;
    public Transform rootHead;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        // update line renderer
        linePoints = new Vector3[points.Count];
        for(int i = 0; i < points.Count; i++)
        {
            linePoints[i] = points[i];
        }
        lr.SetPositions(linePoints);
    }

    private void FixedUpdate()
    {
        points.Add(rootHead.position);
    }
}
