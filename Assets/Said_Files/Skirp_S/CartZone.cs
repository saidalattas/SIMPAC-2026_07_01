using UnityEngine;

public class CartZone : MonoBehaviour
{
    public PlayerData player;  // Drag PlayerData.asset ke sini
    public AudioSource audioSource;
    public AudioClip dropSFX;

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek ini hasil panen
        Harvestable harvest = other.GetComponent<Harvestable>();
        if (harvest != null)
        {
            // Tambah ke inventory
            player.AddItem(harvest.itemId, 1);

            // DAILY MISSION UPDATE
            DailyMissionManager dm = FindObjectOfType<DailyMissionManager>();
            if (dm != null)
            {
                dm.AddProgress(harvest.itemId);
                Debug.Log("Misi harian diperbarui dengan item: " + harvest.itemId);
            }

            //InventoryUI ui = FindObjectOfType<InventoryUI>();
            //if (ui != null) ui.UpdateUI();

            // Suara drop / item masuk gerobak
            if (audioSource && dropSFX)
                audioSource.PlayOneShot(dropSFX);

            // Hapus buah dari dunia
            Destroy(other.gameObject);

            Debug.Log($"{harvest.itemId} ditambahkan ke inventory!");
        }
    }
}
