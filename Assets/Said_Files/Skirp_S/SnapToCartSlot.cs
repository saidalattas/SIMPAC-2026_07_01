using UnityEngine;

public class SnapToCartSlot : MonoBehaviour
{
    public Transform snapPoint;
    public string requiredTag = "Harvestable";
    public bool lockAfterSnap = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        // ambil rigidbody
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;      // BIKIN IKUT GEROBOK
            rb.useGravity = false;
        }

        // pindah posisi
        other.transform.position = snapPoint.position;
        other.transform.rotation = snapPoint.rotation;

        // jadikan child  agar ikut dorongan gerobok
        other.transform.SetParent(snapPoint);

        if (lockAfterSnap)
        {
            Collider col = other.GetComponent<Collider>();
            if (col != null)
                col.enabled = false; // biar ga bisa diambil lagi
        }
    }
}
