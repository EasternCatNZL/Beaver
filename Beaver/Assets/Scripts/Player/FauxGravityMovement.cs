using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement Controller for objects under influence of Faux Gravity
public class FauxGravityMovement : MonoBehaviour
{

    public float moveSpeed = 15.0f; //moveSpeed at which object moves
    public float jumpForce = 200.0f; //amount of acceleration used in jumps

    //bool m_bIsGrounded = true; //checks if object is grounded
    Vector3 m_v3MoveAmount; //amount to GetMoveInput object
    Vector3 m_v3SmoothMoveVelocity; //smooths out movement
    Rigidbody m_Rigid; //reference to this objects rigidbody

    private Animator m_Animator;

    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetMoveInput();
        Jump();
        if(GetComponent<Rigidbody>().velocity.y < 0.0f)
        {
            m_Animator.SetBool("Falling", true);
        }
        else
        {
            m_Animator.SetBool("Falling", false);
        }
    }

    void FixedUpdate()
    {
            Vector3 localMove = transform.TransformDirection(m_v3MoveAmount) * Time.fixedDeltaTime;
            m_Rigid.MovePosition(m_Rigid.position + localMove);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Vine" || collision.gameObject.tag == "VineHead" || collision.gameObject.tag == "VineRoot")
        {
            m_Animator.SetBool("Attack", true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Vine" || collision.gameObject.tag == "VineHead" || collision.gameObject.tag == "VineRoot")
        {
            m_Animator.SetBool("Attack", false);
        }
    }

    //gets movement for controlled object
    void GetMoveInput()
    {
        //get movement inputs
        float inputX = Input.GetAxisRaw("Horizontal");
        //float inputY = Input.GetAxisRaw("Vertical"); //For 3d world

        //set GetMoveInput direction
        Vector3 moveDir = new Vector3(inputX, 0, 0).normalized;
        if(moveDir.x > 0)
        {
            m_Animator.SetBool("Right", true);
            m_Animator.SetBool("Left", false);
        }
        else if(moveDir.x < 0)
        {
            m_Animator.SetBool("Right", false);
            m_Animator.SetBool("Left", true);
        }
        else
        {
            m_Animator.SetBool("Right", false);
            m_Animator.SetBool("Left", false);
        }
        //get the amount to move
        Vector3 targetMoveAmount = moveDir * moveSpeed;

        m_v3MoveAmount = Vector3.SmoothDamp(m_v3MoveAmount, targetMoveAmount, ref m_v3SmoothMoveVelocity, 0.15f);
    }

    //jump functionality
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (CheckGrounded())
            {
                m_Animator.SetTrigger("Jump");
                m_Rigid.AddForce(transform.up * jumpForce);
            }
        }
    }

    //checks to see if object is currently on the ground
    bool CheckGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5.5f + 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}