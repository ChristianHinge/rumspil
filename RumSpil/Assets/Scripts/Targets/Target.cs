
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 0f;
    public void TakeDamage(float amount)
    {
        if (health == 0f)
            health = 50*transform.parent.localScale.x*transform.parent.localScale.y*transform.parent.localScale.z;
        health -= amount;
        if (health <= 0f)
        {
            die();
        }

        void die()
        {
            Destroy(gameObject);
        }
    }
} 