using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SeedController : MonoBehaviour
{
    public GameObject seed;
    public Transform seedSpawnPoint;
    private bool isHeld;
    public Vector3 smallerScale = new Vector3 (0.5f, 0.5f, 0.5f);


    void StartHolding(XRBaseInteractor args)
    {
        isHeld = true;
    }

    void StopHolding(XRBaseInteractor args)
    {
        isHeld = false;
    }

    // Action triggered when the object is clicked while held
    void OnClickAction(XRBaseInteractor args)
    {
        if (isHeld) // Only perform action when the object is held
        {
            // Spawn a single droplet of water
            SpawnSeed();

            // Shrink the seedling after interaction
            ShrinkSeedling();
        }
    }

    // Shrink the seedling when clicked
    void ShrinkSeedling()
    {
        transform.localScale = smallerScale; // Scale down the seedling
    }
    // Instantiate a single water droplet
    void SpawnSeed()
    {
       if (seed != null || seedSpawnPoint != null)
        {
            //Vector3 transForm = new Vector3(0f, 0.5f, 0.5f);

            Instantiate(seed, seedSpawnPoint.position, Quaternion.identity);
            Debug.Log("was spawned");

        }
        else
        {
            Debug.LogError("Seed Prefab or Seed Spawn Point is not assigned.");
        }
    }

    // Check for ground collision to remove the waterPrefab
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Bibit telah menyentuh tanah.");
            if (seed != null)
            {
                Destroy(seed); // Destroy water prefab when it touches the ground
            }
        }
        //Destroy(seed);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to grab events
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(StartHolding);
            grabInteractable.onSelectExited.AddListener(StopHolding);

            // Listen for activation (button press)
            grabInteractable.onActivate.AddListener(OnClickAction);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
