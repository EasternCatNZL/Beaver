using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 15.0f; //moveSpeed player moves at
    public PlayerJump playerJump; //reference to player jump script

    private Vector3 m_v3MoveDir;
    private Rigidbody m_Rigid;

    // Use this for initialization
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        print("Update");
        print(playerJump.IsGrounded());
        if (playerJump.IsGrounded())
        {
            //get movement input
            m_v3MoveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
        }

    }

    void FixedUpdate()
    {
        //GetMoveInput the rigidbody in local space
        if (playerJump.IsGrounded())
        {
            m_Rigid.MovePosition(m_Rigid.position + transform.TransformDirection(m_v3MoveDir) * moveSpeed * Time.deltaTime);
        }
    }
}