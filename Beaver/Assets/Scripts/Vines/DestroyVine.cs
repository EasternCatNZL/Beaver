using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVine : MonoBehaviour
{
    public bool Regrow;

    private GameObject CurrencyManager;

    private GameObject m_Parent;
    private GameObject m_Child;
    private GameObject m_Child2;

    // Use this for initialization
    void Start()
    {
        CurrencyManager = GameObject.FindGameObjectWithTag("CurrencyManager");
    }

    public void PassParent(GameObject _Parent)
    {
        m_Parent = _Parent;
    }


    public void PassChildren(GameObject _Child, GameObject _Child2)
    {
        
        m_Child = _Child;
        m_Child2 = _Child2;
    }

    private void OnCollisionEnter(Collision collision)
    {       
        if(m_Parent) m_Parent.tag = "VineHead";
        if (Regrow && m_Parent)
        {
            m_Parent.GetComponent<Vine>().Regrow();
        }
        DestroySelf();
    }

    //Calls DestroySelf() on its children before destroy the current vine
    public void DestroySelf()
    {
        if (CurrencyManager) CurrencyManager.GetComponent<CurrencyManagement>().AddLogs(1);  
   
        if (m_Child != null)
        {
            m_Child.GetComponent<DestroyVine>().DestroySelf();
        }
        if(m_Child2 != null)
        {
            m_Child2.GetComponent<DestroyVine>().DestroySelf();
        }       
        Destroy(gameObject);
    }

}
