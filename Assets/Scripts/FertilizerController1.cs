using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Pastikan Anda memiliki paket XR Interaction Toolkit terpasang

public class FertilizerCanController : MonoBehaviour
{
    public GameObject waterPrefab; // Prefab untuk air yang keluar
    public Transform waterSpawnPoint; // Tempat keluarnya air
    public float waterRate = 0.5f; // Waktu jeda antar tetes air

    private bool isHeld = false;
    private Coroutine wateringCoroutine;

    void Start()
    {
        // Subscribe to grab events
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(StartHolding);
            grabInteractable.onSelectExited.AddListener(StopHolding);

            // Listen for activation (button press)
            grabInteractable.onActivate.AddListener(StartWatering);
            grabInteractable.onDeactivate.AddListener(StopWatering);
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
        StopWatering(interactor); // Ensure watering stops if the object is released
    }

    void StartWatering(XRBaseInteractor interactor)
    {
        if (isHeld) // Only start watering if the object is currently held
        {
            wateringCoroutine = StartCoroutine(Water());
        }
    }

    void StopWatering(XRBaseInteractor interactor)
    {
        if (wateringCoroutine != null)
        {
            StopCoroutine(wateringCoroutine);
        }
    }

    IEnumerator Water()
    {
        while (true) // Continue as long as the button is held down
        {
            Instantiate(waterPrefab, waterSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(waterRate);
        }
    }
}
