using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurret : MonoBehaviour {

	void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "VineHead")
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
