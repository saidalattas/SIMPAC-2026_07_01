using UnityEngine;
using System.Collections;

public class MarketZone : MonoBehaviour
{
    [Header("UI & Audio")]
    public GameObject marketUI;
    public GameObject Inventory;
    public AudioSource npcAudio;
    public AudioClip welcomeClip, goodbyeClip;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tampilkan Market UI
            marketUI.SetActive(true);
            if (npcAudio && welcomeClip)
                npcAudio.PlayOneShot(welcomeClip);

            // Tampilkan Inventory
            if (Inventory != null)
                Inventory.SetActive(true);

            // Sapa pemain
            BubbleText.Show("Selamat datang di HF Market!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tutup Market UI
            marketUI.SetActive(false);
            if (npcAudio && goodbyeClip)
                npcAudio.PlayOneShot(goodbyeClip);

            // Tutup Inventory
            if (Inventory != null)
                Inventory.SetActive(false);
        }
    }
}
