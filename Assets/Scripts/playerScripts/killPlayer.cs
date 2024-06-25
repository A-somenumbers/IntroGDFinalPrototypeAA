using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip di;
    public static bool dead;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.playSFXclip(di, transform, 1f);
            dead = true;
            Time.timeScale = 0f;
        }
    }
}
