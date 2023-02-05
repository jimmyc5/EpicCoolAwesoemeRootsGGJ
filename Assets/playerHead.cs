using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHead : MonoBehaviour
{
    
    public bool followMouse = false;
    public float distanceToTravel = 20f;
    public float distanceTraveled = 0f;
    public int startPhysicsSteps = 10;
    private bool starting = true;
    public float speed;
    private Camera cam;
    private Rigidbody2D rb;
    private TrailRenderer myTrail;
    private Vector3 lastPosition;

    public GameObject thingToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TrailRenderer>();
        distanceTraveled = 0f;
        followMouse = false;
        rb.isKinematic = false;
        lastPosition = transform.position;
        startPhysicsSteps = 10;
        myTrail.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(starting && startPhysicsSteps < 1)
        {
            rb.isKinematic = true;
            starting = false;
        }
        if (!starting)
        {
            if (followMouse && distanceTraveled < distanceToTravel)
            {
                rb.isKinematic = false;
                Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 target = new Vector3(mouse.x, mouse.y, 0);

                Vector3 temp = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                rb.MovePosition(new Vector3(temp.x, temp.y, 0));
                //distanceTraveled += Mathf.Sqrt(temp.x*temp.x + temp.y*temp.y);
            }
            else
            {
                rb.isKinematic = true;
            }
            
        }
       
        if(distanceTraveled >= distanceToTravel)
        {
            int pos1 = myTrail.positionCount / 3;
            int pos2 = 2 * myTrail.positionCount / 3;
            GameObject.Instantiate(thingToSpawn, myTrail.GetPosition(pos1), Quaternion.identity);
            GameObject.Instantiate(thingToSpawn, myTrail.GetPosition(pos2), Quaternion.identity);
            rb.isKinematic = true;
            enabled = false;

        }
    }
    private void FixedUpdate()
    {
        if (starting)
        {
            startPhysicsSteps--;
        }else if(!followMouse){
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0, 0);
        }
        float dist = Vector3.Distance(transform.position, lastPosition);
        distanceTraveled += dist;
        GameManager.instance.totalDistance += dist;
        Vector3 dir = transform.position - lastPosition;
        if(dir.magnitude > 0.05f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = (Quaternion.AngleAxis(angle, Vector3.forward));
        }
        lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Root")
        {
            Debug.Log("ouch");
        }
    }
}
