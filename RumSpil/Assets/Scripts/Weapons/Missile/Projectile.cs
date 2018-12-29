using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);

    }
    private void Update()
    {

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
