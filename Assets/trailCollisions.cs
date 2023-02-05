using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class trailCollisions : MonoBehaviour
{
    TrailRenderer myTrail;
    EdgeCollider2D myCollider;

    static List<EdgeCollider2D> unusedColliders = new List<EdgeCollider2D>();

    private CircleCollider2D col;
    public int colliderPointOffset = 10;

    void Awake()
    {
        myTrail = this.GetComponent<TrailRenderer>();
        myCollider = GetValidCollider();
        col = this.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        SetColliderPointsFromTrail(myTrail, myCollider);
    }

    //Gets from unused pool or creates one if none in pool
    EdgeCollider2D GetValidCollider()
    {
        EdgeCollider2D validCollider;
        if (unusedColliders.Count > 0)
        {
            validCollider = unusedColliders[0];
            validCollider.enabled = true;
            unusedColliders.RemoveAt(0);
        }
        else
        {
            validCollider = new GameObject("TrailCollider", typeof(EdgeCollider2D)).GetComponent<EdgeCollider2D>();
            //validCollider.isTrigger = true;
            validCollider.gameObject.tag = "Root";
        }
        return validCollider;
    }

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        //avoid having default points at (-.5,0),(.5,0)
        Vector2 point;
        if (trail.positionCount == 0)
        {
            points.Add(transform.position);
            points.Add(transform.position);
            collider.isTrigger = true;
        }
        else for (int position = 0; position < trail.positionCount - colliderPointOffset; position++)
            {
                collider.isTrigger = false;
                point = trail.GetPosition(position);
                // ignores z axis when translating vector3 to vector2
                if (Vector3.Distance(transform.position, new Vector3(point.x,point.y)) > col.radius *0.02f)
                {
                    points.Add(point);
                }
                
            }
        collider.SetPoints(points);
    }

    void OnDestroy()
    {
        if (myCollider != null)
        {
            myCollider.enabled = false;
            unusedColliders.Add(myCollider);
        }
    }
}