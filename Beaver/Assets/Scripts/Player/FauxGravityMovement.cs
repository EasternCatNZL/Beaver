using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement Controller for objects under influence of Faux Gravity
public class FauxGravityMovement : MonoBehaviour {

    public float moveSpeed = 15.0f; //moveSpeed at which object moves
    public float jumpForce = 200.0f; //amount of acceleration used in jumps

    //bool m_bIsGrounded = true; //checks if object is grounded
    Vector3 m_v3MoveAmount; //amount to GetMoveInput object
    Vector3 m_v3SmoothMoveVelocity; //smooths out movement
    Rigidbody m_Rigid; //reference to this objects rigidbody

    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetMoveInput();
        Jump();
    }

    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(m_v3MoveAmount) * Time.fixedDeltaTime;
        m_Rigid.MovePosition(m_Rigid.position + localMove);
    }

    //gets movement for controlled object
    void GetMoveInput()
    {
        //get movement inputs
        float inputX = Input.GetAxisRaw("Horizontal");
        //float inputY = Input.GetAxisRaw("Vertical"); //For 3d world

        //set GetMoveInput direction
        Vector3 moveDir = new Vector3(inputX, 0, 0).normalized;
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
                m_Rigid.AddForce(transform.up * jumpForce);
            }
        }
    }

    //checks to see if object is currently on the ground
    bool CheckGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}