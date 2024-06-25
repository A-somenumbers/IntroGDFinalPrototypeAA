using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private AudioClip t;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Proj"))
        {
            Destroy(other.gameObject);
            AudioManager.Instance.playSFXclip(t, transform, 0.5f);
        }
    }
}
