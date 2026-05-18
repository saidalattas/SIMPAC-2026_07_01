using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Pastikan Anda memiliki paket XR Interaction Toolkit terpasang

public class FertilizerController : MonoBehaviour
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
            grabInteractable.onSelectEntered.AddListener(StartWatering);
            grabInteractable.onSelectExited.AddListener(StopWatering);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    void StartWatering(XRBaseInteractor interactor)
    {
        isHeld = true;
        wateringCoroutine = StartCoroutine(Water());
    }

    void StopWatering(XRBaseInteractor interactor)
    {
        isHeld = false;
        if (wateringCoroutine != null)
        {
            StopCoroutine(wateringCoroutine);
        }
    }

    IEnumerator Water()
    {
        while (isHeld)
        {
            Instantiate(waterPrefab, waterSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(waterRate);
        }
    }
}