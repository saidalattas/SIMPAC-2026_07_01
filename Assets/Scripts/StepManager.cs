using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    public AudioClip[] stepSounds;
    public float stepInterval = 0.5f;
    public ContinuousMoveProviderBase moveProvider;
    private AudioSource audioSource;
    private float stepTimer = 0f;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(moveProvider == null)
        {
            Debug.LogError("Continuous Move Provider is not assigned in StepSoundManager.");
        }

        lastPosition = transform.position;
        Debug.Log(lastPosition);
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        if (movementSpeed > 0.0001f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer > stepInterval)
            {
                PlayStepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    private void PlayStepSound()
    {
        if(stepSounds.Length > 0)
        {
            AudioClip clip = stepSounds[Random.Range(0, stepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Step sound empty");
        }
        
    }
}
