using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManagement : MonoBehaviour {

    public Text LogDisplay;

    //Costs for turrets are store on the CurrencyManagement script
    public float BeaverLauncherCost;

    bool enoughlogs;

    public enum TurretTypes
    {
        BEAVERLAUNCHER,
    };

    public float m_iLogs;//The local currencey of the United Beaver Systems

	// Use this for initialization
	void Start () {
	}

    public bool CanBuyTurret(TurretTypes _eType)
    {
        if(_eType == TurretTypes.BEAVERLAUNCHER)
        {
            if (m_iLogs >= BeaverLauncherCost)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    /*
    public void BuyTurret()
    {
           if(enoughlogs == true)
            {
                m_iLogs -= BeaverLauncherCost;
            }
        if (LogDisplay) LogDisplay.text = m_iLogs.ToString();
    }
    */   
    
    public void BuyTurret(TurretTypes _eType)
    {
        if (_eType == TurretTypes.BEAVERLAUNCHER)
        {
            if (CanBuyTurret(_eType))
            {
                m_iLogs -= BeaverLauncherCost;
                Mathf.Floor(BeaverLauncherCost *= 2.0f);
            }

        }
        if (LogDisplay) LogDisplay.text = m_iLogs.ToString();
    }
    
    public void AddLogs(int _iAmount)
    {
        m_iLogs += _iAmount;
        if(LogDisplay) LogDisplay.text = m_iLogs.ToString();
    }

    public bool LogCheck()
    {
       enoughlogs = false;
        if(m_iLogs >= BeaverLauncherCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
