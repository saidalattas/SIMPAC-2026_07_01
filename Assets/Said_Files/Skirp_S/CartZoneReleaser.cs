using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CartZoneReleaser : MonoBehaviour
{
    public string harvestTag = "Harvestable";
    public Transform dropPoint;

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan objek yang masuk adalah Harvestable
        if (other.CompareTag(harvestTag))
        {
            // 1 Lepas dari parent gerobak
            other.transform.SetParent(null);

            // 2 Re-enable XR Grab supaya bisa diambil kembali
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = true;

            // 3? Aktifkan rigidbody kembali
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // 4 WAJIB: aktifkan collider kembali
            Collider col = other.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            // 5 Pindahkan posisi ke drop area biar rapi
            if (dropPoint != null)
            {
                other.transform.position = dropPoint.position;
                other.transform.rotation = dropPoint.rotation;
            }

            Debug.Log("Item dilepas dari gerobak dan bisa di-grab kembali.");
        }
    }
}
