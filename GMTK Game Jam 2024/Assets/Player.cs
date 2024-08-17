using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialScale;
    private bool leftPressed;
    private bool rightPressed;

    public float targetVelocity;
    public float acceleration;
    private float expectedXVelocity;

    public float maxSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.localScale = initialScale * 2f;
        }
        else
        {
            transform.localScale = initialScale;
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
        if(leftPressed && !rightPressed)
        {
            xInput = -1f;
        }

        float targetX = maxSpeed * xInput;
        float expectedXVelocity = Mathf.MoveTowards(rb.velocity.x, targetX, acceleration * Time.deltaTime);
        rb.velocity = new Vector2(expectedXVelocity, rb.velocity.y);


    }
}
