using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretSpawner : MonoBehaviour {

    public GameObject myTurret;
    //public CurrencyManagement currencyScript;
    public GameObject CurrencyManager;
    //public GameObject Player;

    private Transform playerTransform;
    //private Transform planetTransform;

	// Use this for initialization
	void Start ()
    {
        playerTransform = GameObject.Find("Player").transform;
       //planetTransform = GameObject.Find("Planet").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnTurret();
    }

    void SpawnTurret()
    {
        if(CurrencyManager.GetComponent<CurrencyManagement>().LogCheck() == true && GetComponent<FauxGravityMovement>().CheckGrounded() == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Vector3 playerPosition = playerTransform.position;
                //GameObject turretClone = Instantiate(myTurret, playerTransform.position + planetTransform.position, playerTransform.rotation);
                Vector3 offset = new Vector3(0, -1, 0);
                GameObject turretClone = Instantiate(myTurret, playerTransform.position + offset, playerTransform.rotation);
                CurrencyManager.GetComponent<CurrencyManagement>().BuyTurret(CurrencyManagement.TurretTypes.BEAVERLAUNCHER);
             }
        }
    }
}

