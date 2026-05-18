using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RetainColliderOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isFirst = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Ensure the Rigidbody and Colliders are set properly
        if (rb == null)
        {
            Debug.Log("No Rigidbody found on this object!");
        }
    }

    private void OnEnable()
    {
        // Subscribe to grab and release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
        Debug.Log("Subscribe Event");
    }

    private void OnDisable()
    {
         //Unsubscribe from grab and release events
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
        Debug.Log("Unsubscribe event");
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Make the Rigidbody kinematic when grabbed
        if (rb != null && isFirst == false)
        {
            rb.isKinematic = true;
            isFirst = true;
            Debug.Log("Object Grabbed and kinematic");
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Restore Rigidbody properties on release
        if (rb != null)
        {
            rb.isKinematic = false;
            Debug.Log("Object release and no kinematic");
        }
    }
}
