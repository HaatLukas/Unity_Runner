using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private bool muted;

    public AudioClip jump;  // Dźwięki mają typ AudioClip
    public AudioClip CoinGrab; // Dźwięki mają typ AudioClip
    public AudioClip click; // Dźwięki mają typ AudioClip

    public AudioSource effectsSource; // Ustawienia mają typ AudioSource
    private AudioSource audioSource;


    public void PlayOnceJumpSound()
    {
        if (muted == true) return;
        effectsSource.PlayOneShot(jump, 1f);
    }

    public void PlayOnceClickSound() // Co tu dać do zmiany?
    {
        if (muted == true) return;
        effectsSource.PlayOneShot(click, 1f);
    }

    public void PlayOnceCoinGrabSound() // Co tu dać do zmiany?
    {
        if (muted == true) return;
        effectsSource.PlayOneShot(CoinGrab, 1f);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject); //to powoduje "trwanie" tego obiektu przy zmianie scen
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMuted()
    {
        //negowanie wartosci i przypisanie informajci o tym czy jest dzwiek
        muted = !muted;

        audioSource.mute = muted;
    }

    public bool GetMuted()
    {
        return muted;
    }
}
