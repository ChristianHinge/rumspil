using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rocks.instance.EnterSection(Vector3.zero);
    }

}
