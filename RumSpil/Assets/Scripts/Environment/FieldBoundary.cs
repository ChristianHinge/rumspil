using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBoundary : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter");
            Rocks.instance.EnterSection(other.transform.position);
        }

    }
}
