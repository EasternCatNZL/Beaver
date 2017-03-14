using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public float scrollSpeed; //speed tiles scroll at
    public float tileSize; //size of tile

    private Vector3 m_v3StartPos; //starting pos of tile

    // Use this for initialization
    void Start () {
        m_v3StartPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = m_v3StartPos + Vector3.right * newPos;
	}
}
