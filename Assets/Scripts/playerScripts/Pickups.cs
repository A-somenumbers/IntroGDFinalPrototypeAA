using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.CompareTag("Dash"))
        {
            Destroy(o.gameObject);
        }            
    }
}
