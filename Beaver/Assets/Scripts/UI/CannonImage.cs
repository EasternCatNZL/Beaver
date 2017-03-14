using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonImage : MonoBehaviour {

    public Image image; //reference to image
    public Sprite canBuyImage; //reference to lit image
    public Sprite cantBuyImage; //reference to greyed image
    public CurrencyManagement currencyManager; //reference to currency manager

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currencyManager.CanBuyTurret(CurrencyManagement.TurretTypes.BEAVERLAUNCHER))
        {
            image.sprite = canBuyImage;
        }
        else
        {
            image.sprite = canBuyImage;
        }
	}
}
