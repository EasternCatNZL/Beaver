using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

    public float gravity = -10.0f; //Gravity force used for attraction to object
    public float launchForce = 20.0f; //force of the launch

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Attract(Rigidbody _body)
    {
        //get up direction based on gravity source
        Vector3 gravityUp = (_body.position - transform.position).normalized;
        //direction body is currently facing
        Vector3 bodyUp = _body.transform.up;

        //Apply downwards gravity to body
        _body.AddForce(gravityUp * gravity);
        //allign body up axis with centre of planet
        _body.rotation = Quaternion.FromToRotation(bodyUp, gravityUp) * _body.rotation;

        /*
        //Head towards gravity source with gravity force
        _body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        //get difference in rotation between body and gravity source
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * _body.rotation;
        //GetMoveInput towards target smoothly
        _body.rotation = Quaternion.Slerp(_body.rotation, targetRotation, 50 * Time.deltaTime);
        */
    }

    //public void Jump(Transform _body)
    //{
    //    //get up direction based on gravity source
    //    Vector3 gravityUp = (_body.position - transform.position).normalized;
    //    //direction body is currently facing
    //    Vector3 bodyUp = _body.up;
    //    //launch away from attractor body
    //    _body.GetComponent<Rigidbody>().AddForce(-gravityUp * launchForce * 100);
    //    //get difference in rotation between body and gravity source
    //    Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * _body.rotation;
    //    //GetMoveInput towards target smoothly
    //    _body.rotation = Quaternion.Slerp(_body.rotation, targetRotation, 50 * Time.deltaTime);
    //}
}
