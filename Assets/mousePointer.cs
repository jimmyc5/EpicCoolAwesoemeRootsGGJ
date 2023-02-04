using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePointer : MonoBehaviour
{
    private Camera cam;
    private playerHead currentHead;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 temp = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(temp.x, temp.y);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerHead[] rootHeads = GameObject.FindObjectsOfType<playerHead>();
            if(rootHeads.Length == 0)
            {
                return;
            }
            float minDist = Vector3.Distance(transform.position,rootHeads[0].gameObject.transform.position);
            playerHead closestHead = rootHeads[0];
            for(int i=1; i < rootHeads.Length; i++)
            {
                rootHeads[i].followMouse = false;
                float dist = Vector3.Distance(transform.position, rootHeads[i].gameObject.transform.position);
                if(dist < minDist)
                {
                    minDist = dist;
                    closestHead = rootHeads[i];
                }
            }
            closestHead.followMouse = true;
            currentHead = closestHead;
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentHead.followMouse = false;
        }
    }
}
