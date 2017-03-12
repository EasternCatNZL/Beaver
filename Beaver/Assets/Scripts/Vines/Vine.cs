using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public int m_iID;

    public float GrowthRate;
    public float SplitChance;
    public float MaxTurnAngle;
    public Object VinePrefab;

    public float m_fTimer;
    public Vector3 m_vDirection = new Vector3(0.0f, -1.0f, 0.0f);
    public float m_fNextAngle;
    public bool m_bGrowRight;
    public bool m_bSplit = false;
    public GameObject m_Parent;
    public GameObject m_Child;
    public GameObject m_Child2;

    public bool IDLE = false;
    static int CubeID = 0;
    static int MaxSplits = 5;

    // Use this for initialization
    void Start()
    {
        m_fTimer = Time.time;
    }

    public int GetID() { return m_iID; }

    //Cleans up the child after it has been destroyed
    public bool ClearChild(int _iID)
    {
        if(m_Child.GetComponent<Vine>().GetID() == _iID)
        {
            m_Child = null;
            return true;
        }
        else if (m_Child2.GetComponent<Vine>().GetID() == _iID)
        {
            m_Child2 = null;
            return true;
        }
        Debug.LogError("Parent vine has no such child. Child has incorrect parent object");
        return false;
    }

    public void Regrow()
    {
        IDLE = false;
    }

    public void Create(GameObject _Parent, float _fAngle, bool _bGrowRight)
    {
        //Setting parameter variables
        
        m_iID = CubeID++;
        m_bSplit = false;
        m_Parent = _Parent;
        m_bGrowRight = _bGrowRight;
        GetComponent<DestroyVine>().PassParent(_Parent);
        gameObject.tag = "VineHead";
        //Setting up temp values
        float _fWorldRotation;
        //Set the rotation and position of the current block
        //gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, _fAngle), Space.World); //Takes Parents rotation + new angle to form rotation
        gameObject.transform.parent = _Parent.transform;
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, _fAngle));
        gameObject.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //Get rough direction of travel. In degrees normalised between 0.0f - 1.0f
        _fWorldRotation = gameObject.transform.rotation.eulerAngles.z;
        float _fAngleCorner = _fWorldRotation / 360;
        if (_fAngleCorner > 1.0f) //If the angle is greater than 360. Trim it.
        {
            _fAngleCorner -= Mathf.Floor(_fAngleCorner);
        }
        //Rolling random chance to split
        float splitRoll = Random.Range(0.0f, 1.0f);
        if (splitRoll <= SplitChance && MaxSplits > 0)
        {
            m_bSplit = true;

        }
        //Makes sure the vine trends towards the right and down
        //_fAngleCorner is the WORLD rotation of the object, and is normalised between 0.0f - 1.0f
        //This is to allow better control of the growth path of the vine
        //::Orientation::
        // - 0.0f & 1.0f point downwards
        // - 0.25f points right
        // - 0.5f points upwards
        // - 0.75 points left
        if (m_bGrowRight)
        {
            if (_fAngleCorner > 0.75f)
            {
                m_fNextAngle = Random.Range(0.0f, MaxTurnAngle);
                m_bSplit = false;
            }
            else if(_fAngleCorner > 0.20f)
            {
                m_fNextAngle = Random.Range(0.0f, MaxTurnAngle * -1.0f);
                m_bSplit = false;
            }
            else
            {
                m_fNextAngle = (Random.Range(0.0f, MaxTurnAngle * 2) - MaxTurnAngle);
            }
        }
        //Makes sure the vine trends towards the left and down
        else
        {
            if (_fAngleCorner < 0.25f)
            {
                m_fNextAngle = Random.Range(0.0f, MaxTurnAngle * -1.0f);
                m_bSplit = false;
               
            }
            else if (_fAngleCorner < 0.75f)
            {
                m_fNextAngle = Random.Range(0.0f, MaxTurnAngle);
                m_bSplit = false;
            }
            else
            {
                m_fNextAngle = (Random.Range(0.0f, MaxTurnAngle * 2) - MaxTurnAngle);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IDLE)
        {
            //Checks whether it is possible to preform a split
            if (Time.time - m_fTimer > GrowthRate && m_Child == null && m_Child2 == null && m_bSplit && MaxSplits > 0)
            {
                MaxSplits--;

                GameObject temp = Instantiate(VinePrefab) as GameObject;                            
                GameObject temp2 = Instantiate(VinePrefab) as GameObject;

                temp.GetComponent<Vine>().Create(gameObject, 45, true);
                temp2.GetComponent<Vine>().Create(gameObject, -45, false);

                temp.name = gameObject.name;
                temp2.name = gameObject.name;

                m_Child = temp;
                m_Child2 = temp2;

                GetComponent<DestroyVine>().PassChildren(m_Child, m_Child2);

                if(gameObject.tag != "VineRoot") gameObject.tag = "Vine";
                IDLE = true;
            }
            else if(m_bSplit && MaxSplits <= 0)
            {
                m_bSplit = false;
            }
            //Otherwise continue growing
            else if (Time.time - m_fTimer > GrowthRate && m_Child == null && !m_bSplit)
            {
                m_Child = Instantiate(VinePrefab, gameObject.transform) as GameObject;
                m_Child.GetComponent<Vine>().Create(gameObject, m_fNextAngle, m_bGrowRight);
                m_Child.name = gameObject.name;

                GetComponent<DestroyVine>().PassChildren(m_Child, null);

                if (gameObject.tag != "VineRoot") gameObject.tag = "Vine";
                IDLE = true;
            }          
        }
    }
}


