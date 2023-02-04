using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePointer : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
