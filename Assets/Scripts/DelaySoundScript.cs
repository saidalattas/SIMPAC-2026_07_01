using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySound : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the inspector

    private void Start()
    {
        // Ensure the AudioSource is not null
        if (audioSource == null)
        {
            Debug.LogError("Audio Source is not assigned in the inspector.");
            return;
        }

        // Generate a random delay between 0.5 and 2 seconds
        float delay = Random.Range(2f, 5f);

        // Schedule the audio source to play after the delay
        Invoke("PlayAudio", delay);
    }

    private void PlayAudio()
    {
        // Play the audio source
        audioSource.Play();
    }
}