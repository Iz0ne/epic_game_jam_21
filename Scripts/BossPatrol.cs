using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossPatrol : MonoBehaviour
{
    Animator m_Animator;
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint=0;
    bool reachedEndOfPath = false;
    
    Seeker seeker;
    Rigidbody2D rb;

    public Vector2 direction;
    public Vector2 force;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();

        //Get path
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
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
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        //Generate force vector
        force = direction * speed * Time.deltaTime;
        //Apply force to object
        rb.AddForce(force);

        //Compute distance to next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        //Check if we reached the distance treshold to pass to next waypoint
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        updateSprite();
    }

    void updateSprite()
    {
        
        if (force.y > 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Up");
        }
        else if (force.y < 0 && force.x < 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Left");
        }
        else if (force.y < 0 && force.x > 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Right");
        }
    }

}
