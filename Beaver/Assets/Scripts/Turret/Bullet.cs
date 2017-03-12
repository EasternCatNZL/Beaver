using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float mySpeed = 10.0f;//bullet speed
    public float myRange = 10.0f;//bullets max range
    private float myDist = 0.0f;//for distance bullet traveled
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);//send bullet along local forward axis at mySpeed per second

        myDist += Time.deltaTime * mySpeed;//records distance traveled

        if (myDist >= myRange) //check distance, if greater than range destroy game object
        {
            Destroy(gameObject);
        }
        
    }
    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
