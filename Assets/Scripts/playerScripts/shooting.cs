using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class shooting : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool fire;
    private float timer;
    public float TimeBetweenFire;
    public float TTD;
    [SerializeField] private AudioClip s;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);
        if (!fire)
        {
            timer += Time.deltaTime;
            if (timer > TimeBetweenFire)
            {
                fire = true;
                timer = 0;
            }
        }
        if (!pauseMenu.paused)
        {
            if (Input.GetMouseButtonDown(0) && fire)
            {
                AudioManager.Instance.playSFXclip(s, transform, 0.2f);
                fire = false;
                GameObject b = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                Destroy(b, TTD);

            }
        }


    }
}
