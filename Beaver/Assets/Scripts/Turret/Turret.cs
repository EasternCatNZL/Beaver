using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float Range;

    public GameObject myBullet;
    public float bulletSpeed;
    public float reloadTime;
    public float turnSpeed;
    public float firePauseTime;

    private float nextFireTime;
    private float nextMoveTime;

    public Transform cannonPosition;

    public Transform turretBall;
    private Quaternion desiredRotation;


    public Transform targetTransform;
    public GameObject Target;
    public GameObject[] Targets;

    public Vector3 aimPoint;

    // Use this for initialization
    void Start()
    {
        //Nothing yet
    }

    // Update is called once per frame
    void Update()
    {
        if(Target && Target.tag != "VineHead")
        {
            Target = null;
        }
        if (Target == null)
        {
            Targets = GameObject.FindGameObjectsWithTag("VineHead");
            if (Targets.Length > 0)
            {
                float _fLowestDist = 1000;
                float _fDist;
                foreach(GameObject tar in Targets)
                {
                    _fDist = Vector3.Distance(transform.position, tar.transform.position);
                    if (_fDist < Range && _fDist < _fLowestDist)
                    {
                        _fLowestDist = _fDist;
                        Target = tar;
                        
                    }
                    if(Target)
                    {
                        targetTransform = Target.transform;
                    }
                }
                
            }
        }
        if (targetTransform)
        {
            if (Time.time >= nextMoveTime)
            {

                CalculateAimPosition(targetTransform.position);
                turretBall.rotation = Quaternion.Lerp(turretBall.rotation, desiredRotation, Time.deltaTime * turnSpeed);
            }
            if (Time.time >= nextFireTime)
                FiremyBullet();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && targetTransform == null)
        {
            nextFireTime = Time.time + (reloadTime * 0.5f);
            targetTransform = other.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == targetTransform)
        {
            targetTransform = null;
        }
    }

    void CalculateAimPosition(Vector3 targetTransformPos)
    {
        aimPoint = targetTransformPos - cannonPosition.transform.position;
        if (aimPoint.x < 0)
        {
            desiredRotation = Quaternion.Euler(0.0f, 0.0f, Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), aimPoint));
        }
        else if (aimPoint.x > 0)
        {
            desiredRotation = Quaternion.Euler(0.0f, 0.0f, -Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), aimPoint));

        }
        //desiredRotation = Quaternion.LookRotation(aimPoint);
    }

    void FiremyBullet()
    {
        nextFireTime = Time.time + reloadTime;
        nextMoveTime = Time.time + firePauseTime;
        GameObject bullet = (GameObject)Instantiate(myBullet, cannonPosition.position, cannonPosition.rotation);
        Vector3 bulletVelocity = aimPoint;
        Vector3.Normalize(bulletVelocity); 
        bullet.GetComponent<Rigidbody>().velocity = bulletVelocity * bulletSpeed;

        /*
        foreach (Transform theMuzzlePos in cannonPosition)
        {
            GameObject bullet = (GameObject)Instantiate(myBullet, theMuzzlePos.position, theMuzzlePos.rotation);
        }
        */
    }
}
