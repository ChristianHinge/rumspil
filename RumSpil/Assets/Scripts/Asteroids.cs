using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    GameObject asteroid;
    // Start is called before the first frame update
    void Start()
    {
        Rocks.CreateAsteroidBelt(300f,new Vector3(2000,3000,4000),asteroid,transform);
    }

}
