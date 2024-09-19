using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource SFXSource;

    public AudioClip popSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Optional: Uncomment to keep AudioManager across scene loads
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (SFXSource == null)
        {
            Debug.LogError("AudioSource components missing on AudioManager GameObject.");
        }
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}