using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Laser[] laser;
    [SerializeField] Missile[] missile;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(WeaponSwitching.instance.selectedWeapon == 0)
            {
                foreach (Laser l in laser)
                {
                    // Vector3 pos = transform.position + (transform.forward * l.Distance);
                    l.FireLaser();
                }
            }
            else if (WeaponSwitching.instance.selectedWeapon == 1)
            {
                foreach(Missile m in missile)
                {
                    m.FireMissile();
                }
            }
        }

    }
}