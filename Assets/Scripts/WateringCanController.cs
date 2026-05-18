using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WateringCanController : MonoBehaviour
{
    public GameObject waterPrefab;      // Prefab for visible water droplet
    //public GameObject rainPrefab;       // Prefab for rain particle system
    public Transform waterSpawnPoint;   // Spawn point for both water droplet and rain particles
    public float waterRate = 0.5f;      // Delay between water emissions

    private bool isHeld = false;
    private Coroutine wateringCoroutine;

    //private GameObject rainInstance;    // Reference to the instantiated rain particle system
    // private ParticleSystem rainParticleSystem;
    public int pourThereshold = 45;
    //public Transform origin = null; 
    //public GameObject streamPrefab = null;

    private bool isPouring = false;
    //private Stream currentStream = null;

    public WaterParticleSpawner particleSpawner;

    private void StartWatering(XRBaseInteractor interactor)
    {
        if (isHeld) // Only start watering if the object is currently held
        {
            wateringCoroutine = StartCoroutine(EmitWaterAndRain());
            StartPour();
        }
        print("Start held");
    }

    private void StopWatering(XRBaseInteractor interactor)
    {
        if (wateringCoroutine != null)
        {
            StopCoroutine(wateringCoroutine);
        }

        EndPour();
        print("End");

    }

    private void Update()
    {
       /* bool pourCheck = CalculatePourAngle() < pourThereshold;
        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }*/
    }
    private void StartPour()
    {
        print("Start pour");
        particleSpawner.StartWatering();
        //currentStream = CreateStream();
        //currentStream.Begin();
    }
    private void EndPour()
    {
        //currentStream.End();
        particleSpawner.StopWatering();
        print("Stop");
    }

    [Obsolete]
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
        StopWatering(interactor);  // Ensure watering stops when the can is released
    }


    //private float CalculatePourAngle()
    //{
    //   return transform.forward.y * Mathf.Rad2Deg;
    //}
    private float CalculatePourAngle()
    {
        float pourAngle = transform.forward.y * Mathf.Rad2Deg;
        //Debug.Log("Pour angle: " + pourAngle);
        return pourAngle;
    }

    //private Stream CreateStream()
    //{
    //    Debug.Log("Origin position: " + origin.position);
    //    GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
    //    Debug.Log("create streamobject");
    //    Debug.Log(streamPrefab.name);
    //    return streamObject.GetComponent<Stream>();
    //}

    IEnumerator EmitWaterAndRain()
    {
        while (true) // Continue as long as the button is held down
        {
            // Instantiate the water droplet prefab at the water spawn point
            Instantiate(waterPrefab, waterSpawnPoint.position, waterSpawnPoint.rotation);

            yield return new WaitForSeconds(waterRate);
        }
    }
}
