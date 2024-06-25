using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void playSFXclip(AudioClip audioclip, Transform spawn, float volume)
    {
        AudioSource source = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
        source.clip = audioclip;
        source.volume = volume;
        source.Play();
        float cliplength = source.clip.length;
        Destroy(source.gameObject, cliplength );
    }
}
