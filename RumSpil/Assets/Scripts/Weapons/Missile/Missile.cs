using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField] float MisfireDelay = 2f;
    public GameObject Projectile;
    bool MisCanFire = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        MisCanFire = true;
        
    }

    // Update is called once per frame
    
    
    public void FireMissile()
    {
        if (MisCanFire)
        {
            Debug.Log("missilefires");
           
            Instantiate(Projectile, transform.position, transform.rotation);
            MisCanFire = false;
            Invoke("CanFire", MisfireDelay);
            
        }
    }

    

    void CanFire()
    {
        MisCanFire = true;
    }
    
}
