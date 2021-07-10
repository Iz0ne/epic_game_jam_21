using UnityEngine;

public class Player_Dodo : MonoBehaviour
{
    Animator m_Animator;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey("down"))
        {
            GetComponent<Rigidbody2D>().velocity= new Vector3(0, -1, 0);
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Down");
        }
        else if (Input.GetKey("up"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1, 0);
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Up");
        }
        else if (Input.GetKey("right"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(1, 0, 0);
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Right");
        }
        else if (Input.GetKey("left"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 0, 0);
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Left");
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
    }
}
