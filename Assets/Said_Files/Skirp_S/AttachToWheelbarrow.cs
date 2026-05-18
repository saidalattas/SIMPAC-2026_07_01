using UnityEngine;

public class AttachToWheelbarrow : MonoBehaviour
{
    public string requiredTag = "Harvestable";
    public Transform attachPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        Rigidbody rb = other.attachedRigidbody;
        Collider col = other.GetComponent<Collider>();

        // LOCK object ke gerobak (tanpa mematikan collider!)
        if (rb != null)
        {
            rb.isKinematic = true;   // biar tidak jatuh
            rb.useGravity = false;
        }

        // col.enabled = false;  //  Jangan dimatikan!  FIX
        // collider tetap aktif biar CartZone bisa baca

        // snap posisi ke slot
        other.transform.SetParent(attachPoint);
        other.transform.localPosition = Vector3.zero;
        other.transform.localRotation = Quaternion.identity;

        Debug.Log("Item berhasil dipasang di gerobak.");
    }
}
