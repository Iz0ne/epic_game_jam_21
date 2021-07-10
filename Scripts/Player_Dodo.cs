using UnityEngine;

public class Player_Dodo : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody2D m_Rigid2D;

    [SerializeField] float m_speed = 2;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey("down"))
        {
            if (Input.GetKey("left"))
            {
                m_Rigid2D.velocity = new Vector3(-1, -1, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Down");
                m_Animator.SetTrigger("Left");
            }
            else if (Input.GetKey("right"))
            {
                m_Rigid2D.velocity = new Vector3(1, -1, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Down");
                m_Animator.SetTrigger("Right");
            }
            else if (Input.GetKey("up"))
            {
                m_Rigid2D.velocity = new Vector3(0, 0, 0);
                m_Animator.ResetTrigger("Down");
                m_Animator.SetTrigger("Idle");
            }
            else
            {
                m_Rigid2D.velocity = new Vector3(0, -1, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Idle");
                m_Animator.SetTrigger("Down");
            }
        }
        else if (Input.GetKey("up"))
        {
            if (Input.GetKey("left"))
            {
                m_Rigid2D.velocity = new Vector3(-1, 1, 0).normalized * m_speed;
                m_Animator.SetTrigger("Up");
            }
            else if (Input.GetKey("right"))
            {
                m_Rigid2D.velocity = new Vector3(1, 1, 0).normalized * m_speed;
                m_Animator.SetTrigger("Up");
            }
            else if (Input.GetKey("down"))
            {
                m_Rigid2D.velocity = new Vector3(0, 0, 0);
                m_Animator.ResetTrigger("Up");
                m_Animator.SetTrigger("Idle");
            }
            else
            {
                m_Rigid2D.velocity = new Vector3(0, 1, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Idle");
                m_Animator.SetTrigger("Up");
            }
        }
        else if (Input.GetKey("right"))
        {
            if (Input.GetKey("left"))
            {
                m_Rigid2D.velocity = new Vector3(0, 0, 0);
                m_Animator.ResetTrigger("Right");
                m_Animator.SetTrigger("Idle");
            }
            else if (Input.GetKey("up"))
            {
                m_Rigid2D.velocity = new Vector3(1, 1, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Right");
                m_Animator.SetTrigger("Up");
            }
            else if (Input.GetKey("down"))
            {
                m_Rigid2D.velocity = new Vector3(1, -1, 0).normalized * m_speed;
            }
            else
            {
                m_Rigid2D.velocity = new Vector3(1, 0, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Idle");
                m_Animator.SetTrigger("Right");
            }
        }
        else if (Input.GetKey("left"))
        {
            if (Input.GetKey("right"))
            {
                m_Rigid2D.velocity = new Vector3(0, 0, 0);
                m_Animator.ResetTrigger("Left");
                m_Animator.SetTrigger("Idle");
            }
            else if (Input.GetKey("up"))
            {
                m_Rigid2D.velocity = new Vector3(-1, 1, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Left");
                m_Animator.SetTrigger("Up");
            }
            else if (Input.GetKey("down"))
            {
                m_Rigid2D.velocity = new Vector3(-1, -1, 0).normalized * m_speed;
            }
            else
            {
                m_Rigid2D.velocity = new Vector3(-1, 0, 0).normalized * m_speed;
                m_Animator.ResetTrigger("Idle");
                m_Animator.SetTrigger("Left");
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            m_Animator.ResetTrigger("Down");
            m_Animator.ResetTrigger("Up");
            m_Animator.ResetTrigger("Left");
            m_Animator.ResetTrigger("Right");
            m_Animator.SetTrigger("Idle");
        }

        if (Input.GetKeyDown("space"))
        {
            m_speed = m_speed * 2;
        }

        if (Input.GetKeyUp("space"))
        {
            m_speed = m_speed / 2;
        }

        if (Input.GetKeyDown("z"))
        {
            m_speed = m_speed * 5;
        }

        if (Input.GetKeyUp("z"))
        {
            m_speed = m_speed / 5;
        }
    }
}
