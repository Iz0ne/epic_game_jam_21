using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossPatrol : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint=0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //Get path
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Check if path is valid
        if (path == null)
        {
            return;
        }

        //Check that there are remaining point in the path
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //Generate Direction vector
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Debug.Log(direction);

        //Generate force vector
        Vector2 force = direction * speed * Time.deltaTime;
        Debug.Log(force);
        //Apply force to object
        rb.AddForce(force);

        //Compute distance to next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        //Check if we reached the distance treshold to pass to next waypoint
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Debug.Log(currentWaypoint);
    }
}
