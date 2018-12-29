using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionDetection : MonoBehaviour
{
    public float damage = 50f;
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(gameObject.name);
        Target target = collisionInfo.gameObject.GetComponent<Target>();
        if (target != null)
            target.TakeDamage(damage);
        Destroy(gameObject);    
        
    }
}
