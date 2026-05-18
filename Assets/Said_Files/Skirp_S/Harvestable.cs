using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public string itemId = "Corn";  // diisi dari Inspector

    private bool harvested = false;

    // Fungsi ini kamu panggil saat tanaman dipanen
    public void Harvest()
    {
        if (harvested) return;
        harvested = true;

        // Tambahkan ke Inventory
        PlayerData player = FindObjectOfType<PlayerData>();
        if (player != null)
        {
            player.AddItem(itemId);
        }

        // Update Daily Mission
        DailyMissionManager dm = FindObjectOfType<DailyMissionManager>();
        if (dm != null)
        {
            dm.AddProgress(itemId);
        }

        Debug.Log("Tanaman dipanen: " + itemId);

        // Optional: destroy tanaman setelah dipanen
        Destroy(gameObject);
    }
}
