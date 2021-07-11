using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class charactersPathFinding : MonoBehaviour
{
    //Animation
    Animator m_Animator;
    
    //Path
    public Transform[] targetArray;
    private int targetIdx;
    public Transform target;
    float targetDistance;
    public float nextWaypointDistance = 3f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    //Movements
    Path path;
    public float speed = 200f;
    public Vector2 direction;
    public Vector2 force;
    private bool isBlocked;
    private float blockedTimer = 0;

    //Task
    public float minTaskTime;
    public float maxTaskTime;
    private float taskTime;
    private float waitTime = 0;
    private bool isDoingTask = false;
    
    

    Seeker seeker;
    Rigidbody2D rb;

    

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        targetIdx = Random.Range(0, targetArray.Length);
        target = targetArray[targetIdx];
        taskTime = Random.Range(minTaskTime, maxTaskTime);

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
        //Acquire new target replace
        if (false)
        {
            //Code to acquire player in the detection cone
        }
        else if (reachedEndOfPath)
        {
            doTask();
        }
        


        //Check if path is valid
        if (path == null)
        {
            return;
        }

        //Check that there are remaining point in the path
        if (currentWaypoint >= path.vectorPath.Count)
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
        targetDistance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        //Check if we reached the distance treshold to pass to next waypoint
        if(targetDistance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        checkBlocked();
        updateSprite();
    }

    void doTask()
    {
        waitTime += Time.deltaTime;
        if (waitTime > taskTime)
        {
            //Assign new random task
            int rdmDestPoint = Random.Range(0, targetArray.Length);
            if (targetIdx != rdmDestPoint)
            {
                targetIdx = rdmDestPoint;
                target = targetArray[targetIdx];
                waitTime = 0;
                taskTime = Random.Range(minTaskTime, maxTaskTime);
                isDoingTask = false;
            }
        }
    }

    void checkBlocked()
    {
        //Debug.Log("Taks: "+ isDoingTask + " Blocked Timer: " + blockedTimer + " Velocity: " + rb.velocity.magnitude);
        if (reachedEndOfPath==false && rb.velocity.magnitude < 0.09f)
        {
            blockedTimer += Time.deltaTime;
        }
        else           
        {
            blockedTimer = 0;
        }

        if (blockedTimer > 0.5f)
        {
            int rdmDestPoint = Random.Range(0, targetArray.Length);
            if (targetIdx != rdmDestPoint)
            {
                targetIdx = rdmDestPoint;
                target = targetArray[targetIdx];
                blockedTimer = 0;
                taskTime = Random.Range(minTaskTime, maxTaskTime);
            }
        }
    }

    void updateSprite()
    {
        if (force.normalized.y > 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Up");
        }
        else if (force.normalized.y < 0 && force.normalized.x < 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Left");
        }
        else if (force.normalized.y < 0 && force.normalized.x > 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Right");
        }
    }

}
