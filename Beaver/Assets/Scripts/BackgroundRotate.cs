using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotate : MonoBehaviour {
	
    public float RotationSpeed;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.down, RotationSpeed);
	}
}
