using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BibitTomatController : MonoBehaviour
{
    public GameObject waterPrefab; // Prefab untuk air yang keluar
    public Transform waterSpawnPoint; // Tempat keluarnya air
    public Vector3 smallerScale = new Vector3(0.5f, 0.5f, 0.5f); // Skala lebih kecil untuk bibit tomat

    private bool isHeld = false;

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

    void StartHolding(XRBaseInteractor interactor)
    {
        isHeld = true;
    }

    void StopHolding(XRBaseInteractor interactor)
    {
        isHeld = false;
    }

    // Action triggered when the object is clicked while held
    void OnClickAction(XRBaseInteractor interactor)
    {
        if (isHeld) // Only perform action when the object is held
        {
            // Spawn a single droplet of water
            SpawnWater();

            // Shrink the seedling after interaction
            ShrinkSeedling();
        }
    }

    // Instantiate a single water droplet
    void SpawnWater()
    {
        if (waterPrefab != null && waterSpawnPoint != null)
        {
            Instantiate(waterPrefab, waterSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Water Prefab or Water Spawn Point is not assigned.");
        }
    }

    // Shrink the seedling when clicked
    void ShrinkSeedling()
    {
        transform.localScale = smallerScale; // Scale down the seedling
    }

    // Check for ground collision to remove the waterPrefab
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("BibitTomat telah menyentuh tanah.");
            if (waterPrefab != null)
            {
                Destroy(waterPrefab); // Destroy water prefab when it touches the ground
            }
        }
    }
}
