using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement Controller for objects under influence of Faux Gravity
public class FauxGravityMovement : MonoBehaviour
{

    public float moveSpeed = 15.0f; //moveSpeed at which object moves
    public float jumpForce = 200.0f; //amount of acceleration used in jumps
    public float xBoundries = 9.0f; //horizontal boundry border
    public float objectHeight = 2.0f; //height of the object
    public float rayVariation = 0.2f; //wiggle room for ray calculation
    //public Transform planet;
    //public float distanceBetween = 0.0f;

    //bool m_bIsGrounded = true; //checks if object is grounded
    Vector3 m_v3MoveAmount; //amount to GetMoveInput object
    Vector3 m_v3SmoothMoveVelocity; //smooths out movement
    Rigidbody m_Rigid; //reference to this objects rigidbody
    FauxGravityBody m_fauxBody; //reference to faux body
    bool m_bCanJump = true; //checks whether can jump
    bool m_bCanAttack = true; //checks whether can attack
    public bool m_bFalling = false; //checks if falling
    public bool m_bRising = false; //check if rising
    private Animator m_Animator; //reference to animator on object

    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_fauxBody = GetComponent<FauxGravityBody>();
    }

    void Update()
    {
        GetMoveInput();
        Jump();
        if(m_Rigid.velocity.y < 0.0f)
        {
            m_Animator.SetBool("Falling", true);
            //if reaching ground
            if (m_bRising)
            {
                m_bFalling = true;
                m_bRising = false;
            }
        }
        else
        {
            m_Animator.SetBool("Falling", false);

        }
        if (m_bFalling)
        {
            Landing();
        }

        //if (CheckGrounded())
        //{
        //    print("On Ground");
        //}
        //DebugRay();
        //distanceBetween = Mathf.Round(Vector3.Distance(transform.position, planet.position));
        
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
            m_Animator.SetTrigger("Attacking");
        }
    }

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Vine" || collision.gameObject.tag == "VineHead" || collision.gameObject.tag == "VineRoot")
    //    {
    //        m_Animator.SetTrigger("Attacking");
    //    }
    //}

    //gets movement for controlled object
    void GetMoveInput()
    {
        //get movement inputs
        float inputX = Input.GetAxisRaw("Horizontal");
        //float inputY = Input.GetAxisRaw("Vertical"); //For 3d world

        //set GetMoveInput direction
        Vector3 moveDir = new Vector3(inputX, 0, 0).normalized;

        //change animations based on horizontal input
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

        Vector3 targetMoveAmount = new Vector3(0,0,0);

        //get the amount to move, limited by boundries
        if (AtLeftBoundry())
        {
            //if input is not left
            if (moveDir.x > 0)
            {
                targetMoveAmount = moveDir * moveSpeed;
            }
        }
        else if (AtRightBoundry())
        {
            //if input is not right
            if (moveDir.x < 0)
            {
                targetMoveAmount = moveDir * moveSpeed;
            }
        }
        //else no restrictions
        else
        {
            targetMoveAmount = moveDir * moveSpeed;
        }
        m_v3MoveAmount = Vector3.SmoothDamp(m_v3MoveAmount, targetMoveAmount, ref m_v3SmoothMoveVelocity, 0.15f);
    }

    //jump functionality
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //if currently on ground, then jump
            if (CheckGrounded() && m_bCanJump)
            {
                m_Animator.SetTrigger("Jump");
                m_Rigid.AddForce(transform.up * jumpForce);
                m_bCanJump = false;
                m_bRising = true;
            }
        }
    }

    //checks to see if object is currently on the ground
    bool CheckGrounded()
    {
        bool onGround = false;
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        //if ray detects ground
        if (Physics.Raycast(ray, out hit, objectHeight / 2 + rayVariation))
        {
            //print(hit.distance);
            //check if distance is less then object height
            if (hit.collider.tag == "Ground")
            {
                onGround = true;
            }
            //if (hit.distance <= objectHeight / 2 + rayVariation)
            //{
            //    onGround = true;
            //}
        }
        return onGround;
    }

    //checks if object has reached left boundry
    bool AtLeftBoundry()
    {
        bool atEdge = false;
        //check if reached left edge
        if (transform.position.x < -xBoundries)
        {
            atEdge = true;
        }
        return atEdge;
    }

    //checks if object has reached right boundry
    bool AtRightBoundry()
    {
        bool atEdge = false;
        //check if reached right edge
        if (transform.position.x > xBoundries)
        {
            atEdge = true;
        }
        return atEdge;
    }

    //logic for attacking
    void Attack()
    {
        //stops object in the air
        m_Rigid.velocity = Vector3.zero;
        //change to attacking
        m_Animator.SetTrigger("Attacking");
        //change 
    }

    //animation event logic for landing
    void Landing()
    {
        if (CheckGrounded() && m_bFalling && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            //change to landing
            m_Animator.SetTrigger("Landing");
            m_bFalling = false;
        }
    }

    //change ready to jump after landing
    void ReadyToJump()
    {
        m_bCanJump = true;
        
    }

    void DebugRay()
    {
        
        Debug.DrawRay(transform.position, -transform.up * (objectHeight + rayVariation));

    }
}