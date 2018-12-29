using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class LaserEnemy : MonoBehaviour
{
    LineRenderer lr;
    bool canFire = false;
    [SerializeField] float LaserOffTime = .5f;
    [SerializeField] float maxDistance = 1000f;
    [SerializeField] float fireDelay = 2f;
    public float damage = 10f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

    }

    private void Start()
    {
        lr.enabled = false;
        canFire = true;
    }


    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.blue);
    }


    Vector3 CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;

        if (Physics.Raycast(transform.position,fwd, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                 
            }
            return hit.point;
        }                        
           
        return transform.position + (transform.forward * maxDistance);
    }



    public void FireLaser()
    {
        if (canFire)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, CastRay());
            lr.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", LaserOffTime);
            Invoke("CanFire", fireDelay);
        }

        
    }

    void TurnOffLaser()
    {
        lr.enabled = false;
        
    }
    public float Distance
    {
        get { return maxDistance; }
    }
    void CanFire()
    {
        canFire = true;
        
    }
}
    