using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    LineRenderer lr;
    bool canFire = false;

    [SerializeField] float LaserOffTime = .5f;
    [SerializeField] float maxDistance = 10000f;
    [SerializeField] float fireDelay = 2f;
    public float damage = 10f;
    



    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    private void Start()
    {
        lr.enabled = false; //slukker laser render
        canFire = true; // der kan skydes
    }


    private void Update()
    {
     
      Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.blue); //debug raycast, kan kun ses i scene editor
        
    }


    Vector3 CastRay()
    {
        RaycastHit hit; // hit var, gemmer info om object der er ramt
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance; 

        if (Physics.Raycast(transform.position, fwd, out hit)) //hvis den rammer noget
        {
            Target target = hit.transform.GetComponent<Target>(); //checkr om dne har target script
            if (target != null)
            {
                target.TakeDamage(damage); //tager skade hvis den har target scriptet

            }
            return hit.point;
        }

        return transform.position + (transform.forward * maxDistance);



    }
    public void FireLaser()
    {
        if (canFire)
        {
            lr.SetPosition(0, transform.position); //start pos for laser er laser object på skib
            lr.SetPosition(1, CastRay());// slut punkt for laser er der hvor castray() ender/rammer
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
    