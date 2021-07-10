using UnityEngine;

public class ColleaguePatrol : MonoBehaviour
{
    Animator m_Animator;
    public float speed;
    public float taskTime;
    public Transform[] waypoints;
    private Transform target;
    private int destPoint;
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        int rdmDestPoint = Random.Range(0, waypoints.Length);
        destPoint = rdmDestPoint;
        target = waypoints[destPoint];
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position)<0.3f)
        {
            doTask();
        }
    }

    void doTask()
    {
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
            }
        }

    }
}
