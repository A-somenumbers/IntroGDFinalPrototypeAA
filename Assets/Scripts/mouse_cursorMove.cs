using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseMove2D : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
   
    private void Update()
    {
        if (!pauseMenu.paused) 
        {
            Vector3 mouseWorldpos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldpos.z = 0;
            transform.position = mouseWorldpos; 
        }
        

        
    }
}

