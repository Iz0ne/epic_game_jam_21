using UnityEngine;

public class ColleaguePatrol : MonoBehaviour
{
    Animator m_Animator;
    public float speed;
    public float minTaskTime;
    public float maxTaskTime;

    public Transform[] waypoints;
    private Transform target;
    private int destPoint;
    private float waitTime;
    private float taskTime;
    private Vector3 direction;

    private bool isDoingTask;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        int rdmDestPoint = Random.Range(0, waypoints.Length);
        destPoint = rdmDestPoint;
        target = waypoints[destPoint];
        waitTime = 0;
        taskTime = Random.Range(minTaskTime, maxTaskTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
       
        if (Vector3.Distance(transform.position, target.position)<0.1f)
        {
            doTask();
        }
        
        updateSprite();
    }

    void doTask()
    {
        isDoingTask = true;
        waitTime += Time.deltaTime;
        if (waitTime > taskTime)
        {
            //Assign new random task
            int rdmDestPoint = Random.Range(0, waypoints.Length);
            if (destPoint != rdmDestPoint)
            {
                destPoint = rdmDestPoint;
                target = waypoints[destPoint];
                waitTime = 0;
                taskTime = Random.Range(minTaskTime, maxTaskTime);
                isDoingTask = false;
            }
        }
    }

    void updateSprite()
    {
        if (isDoingTask == true)
        {
            m_Animator.ResetTrigger("Down");
            m_Animator.ResetTrigger("Up");
            m_Animator.ResetTrigger("Left");
            m_Animator.ResetTrigger("Right");
            m_Animator.SetTrigger("Idle");
        }
        else if (direction.normalized.y > 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Up");
        }
        else if (direction.normalized.y < 0 && direction.normalized.x < 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Left");
        }
        else if (direction.normalized.y < 0 && direction.normalized.x > 0)
        {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Right");
        }
    }

}
