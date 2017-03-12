using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

    public FauxGravityAttractor attractor; //reference to attractor
    //public PlayerJump playerJump; //reference to playerjump

    private Rigidbody m_MyRigid; //this objects rigidbody

	// Use this for initialization
	void Start () {
        //get reference to this objects rigidbody
        m_MyRigid = GetComponent<Rigidbody>();
        //prevent own rotation -> no falling over
        m_MyRigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        //turn off gravity for this object
        m_MyRigid.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        //if (Input.GetKeyDown(KeyCode.Space) && playerJump.IsGrounded())
        //{
        //    attractor.Jump(m_MyTransform);
        //    //playerJump.Jump();
        //}
        //check that there is something to pull body towards
        if (attractor)
        {
            attractor.Attract(m_MyRigid);
        }
    }
}
