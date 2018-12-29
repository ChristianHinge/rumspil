 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public Transform ParentT;
    public Transform myT;

    private void Start()
    {
        myT.position = ParentT.position;
    }
    // Update is called once per frame
    void Update()
    {
        RepositionPlayer();
    }

    void RepositionPlayer()
    {
        if (ParentT.position != myT.position)
        {
            myT.position = ParentT.position;
        }
    }
}
    