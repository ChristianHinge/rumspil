using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSystem : MonoBehaviour
{
    [SerializeField]
    Laser[] lasers;
    [SerializeField] float aimPrecision;
    [SerializeField] float shootingDistance;
    Vector3 difference;
    Transform target;
    void Start(){
        target = FindObjectOfType<PlayerInput>().transform;
        Fire();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = target.position-transform.position;
        transform.rotation = Quaternion.LookRotation( difference + difference.magnitude*Vector3.one*Random.Range(0f,1f)/(10*aimPrecision),Vector3.up);
    }
    void Fire(){
        if (difference.magnitude > shootingDistance)
            return;
        foreach (Laser laser in lasers)
        {
            lasers[0].FireLaser();
            Invoke("Fire",0.1f);
        }
    }
}
