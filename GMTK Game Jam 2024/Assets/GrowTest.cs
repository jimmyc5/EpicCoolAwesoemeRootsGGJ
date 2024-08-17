using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTest : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    private float targetVelocity = 0f;

    public float FrameForce;
    public int numPoints;
    public float radius;
    private List<PointMass> points;

    private bool leftPressed;
    private bool rightPressed;

    public float betweenPointFrequency;

    public float outwardPressure = 0f;

    public GameObject PointMassPrefab;
    // Start is called before the first frame update
    void Start()
    {
        points = new List<PointMass>();

        //float TAU = 2 * Mathf.PI;

        //for (int currentPoint = 0; currentPoint < sides; currentPoint++)
        //{
        //    float currentRadian = ((float)currentPoint / sides) * TAU;
        //    float x = Mathf.Cos(currentRadian) * radius;
        //    float y = Mathf.Sin(currentRadian) * radius;
        //    polygonRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        //}
        //polygonRenderer.loop = true;
        float TAU = 2 * Mathf.PI;

        for(int currentPoint = 0; currentPoint < numPoints; currentPoint++)
        {
            PointMass pt = new PointMass();

            GameObject newPoint = GameObject.Instantiate(PointMassPrefab, transform);
            pt.go = newPoint;

            float currentRadian = ((float)currentPoint / numPoints) * TAU;
            float x = Mathf.Cos(currentRadian) * radius;
            float y = Mathf.Sin(currentRadian) * radius;
            newPoint.transform.localPosition = new Vector3(x, y, 0);
            pt.framePoint = new Vector3(x, y, 0);

            Rigidbody2D rb = newPoint.GetComponent<Rigidbody2D>();
            pt.rb = rb;

            points.Add(pt);
        }

        for(int i = 0; i< points.Count; i++)
        {
            for(int j = i + 1;j < points.Count; j++)
            {
                PointMass point = points[i];
                PointMass otherPoint = points[j];

                SpringJoint2D spring = point.go.AddComponent<SpringJoint2D>();
                spring.connectedBody = otherPoint.rb;
                spring.frequency = betweenPointFrequency;
                spring.distance = Vector2.Distance(point.go.transform.position, otherPoint.go.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            outwardPressure = 700f;
        }
        else
        {
            outwardPressure = 0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            leftPressed = true;
        }
        else
        {
            leftPressed = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rightPressed = true;
        }
        else
        {
            rightPressed = false;
        }
    }
    private void FixedUpdate()
    {
        float xInput = 0;
        if (rightPressed && !leftPressed)
        {
            xInput = 1f;
        }
        if (leftPressed && !rightPressed)
        {
            xInput = -1f;
        }

        Vector2 center = new Vector2(0f, 0f);

        foreach (PointMass point in points)
        {
            center += new Vector2(point.go.transform.localPosition.x, point.go.transform.localPosition.y);
        }
        center = center / (float)points.Count;
        foreach(PointMass point in points)
        {
            Vector2 localPos = new Vector2(point.go.transform.localPosition.x, point.go.transform.localPosition.y);
            point.rb.AddForce(FrameForce * ((point.framePoint + center) - localPos));

            point.rb.AddForce(outwardPressure * (point.framePoint));

            float targetX = maxSpeed * xInput;
            float expectedXVelocity = Mathf.MoveTowards(point.rb.velocity.x, targetX, acceleration * Time.deltaTime);
            point.rb.velocity = new Vector2(expectedXVelocity, point.rb.velocity.y);
        }
    }
}

public class PointMass
{
    public GameObject go;
    public Rigidbody2D rb;
    public Vector2 framePoint;
}
