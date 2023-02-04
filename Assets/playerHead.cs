using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHead : MonoBehaviour
{
    public float speed;
    private Camera cam;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = new Vector3(mouse.x, mouse.y, 0);
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.MoveRotation(Quaternion.AngleAxis(angle, Vector3.forward));
        Vector3 temp = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        rb.MovePosition(new Vector3(temp.x, temp.y, 0));
    }
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Root")
        {
            Debug.Log("ouch");
        }
    }
}
