using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVinesCollided : MonoBehaviour {

    //public float planetRadius; //radius of planet

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //CountVineHeadsInPlanet();
        print(CountVineHeadsInPlanet());
	}

    //void OnTriggerStay(Collider _other)
    //{
    //    if (_other.CompareTag("VineHead"))
    //    {

    //    }
    //}

    //counts the number of vine heads in planet
    int CountVineHeadsInPlanet()
    {
        int numberOfHeads = 0;
        //get all colliders within planet area
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //if object is vine head, count
            if (hitColliders[i].CompareTag("VineHead"))
            {
                numberOfHeads++;
            }
        }

        return numberOfHeads;
    }
}
