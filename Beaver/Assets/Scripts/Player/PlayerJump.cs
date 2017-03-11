using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public float jumpForce = 10.0f; //amount of force used in jumps
    public float characterHeight = 2.0f; //height of the character
    public float rayVariation = 0.2f; //float to add more length to raycast, to give more wiggle room in calculations
    public Transform floorOrigin; //reference to the transform that this object needs to land on
    //public FauxGravityAttractor fuaxAttractor; //reference to faux gravity attractor

    //private RaycastHit m_RayHit; //gets information from raycast
    //private bool m_bOnGround = false; //checks if object is currently grounded
    private Rigidbody m_Rigid; //reference to this objects rigidbody

    // Use this for initialization
    void Start () {
        m_Rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        //IsGrounded();
        CheckRay();
	}

    void FixedUpdate()
    {
       // Jump();
    }

    //checks if the character can jump
    public bool IsGrounded()
    {
        //bool check to see if grounded
        bool isGrounded = false;
        //get direction towards attraction body
        Vector3 directionToPlanet = transform.position - floorOrigin.position;
        //if raycast detects ground below character then can jump
        if (Physics.Raycast(transform.position, -transform.up, characterHeight / 2 + rayVariation))
        {
            isGrounded = true;
        }

        return isGrounded;
    }

    //jumping function
    public void Jump()
    {
        //check input recieved
        if (Input.GetButton("Jump"))
        {
            //check is on ground and input recieved
            if (IsGrounded())
            {
                //DEBUG ##
                print("Can Jump");
                //launch the object with force upwards
                m_Rigid.AddRelativeForce(transform.up * jumpForce * 100);
            }
            else
            {
                print("Can't jump");
            }
        }
    }

    //debug func
    private void CheckRay()
    {
        //DEBUG ## draw ray
        //Debug.DrawRay(transform.position, new Vector3(transform.position.x, transform.position.y - characterHeight / 2, transform.position.z), Color.white);
        Debug.DrawRay(transform.position, -transform.up, Color.white);
        Debug.DrawRay(transform.position, transform.up, Color.red);
    }
}
