using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    private Transform player;
    private Transform pointer;
    public float var = 0.25f;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        pointer = GameObject.FindWithTag("Pointer").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPos = transform.position;
        tempPos.x = (pointer.position.x - player.position.x)*var+player.position.x;
        tempPos.y = (pointer.position.y - player.position.y) * var + player.position.y;
        transform.position = tempPos;
    }
} 
