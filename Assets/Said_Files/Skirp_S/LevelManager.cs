using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text levelText;
    public Slider xpSlider;
    public TMP_Text xpText;

    [Header("Unlock Area Event")]
    public PadlockController padlockMarket;

    [Header("Player Data")]
    public PlayerData player;   //  DRAG KE INSPECTOR

    [Header("Audio")]
    public AudioClip levelUpSFX;   // Audio
    private AudioSource audioSource;


    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PLAYER DATA BELUM DI-ASSIGN DI INSPECTOR!");
            return;
        }

        audioSource = GetComponent<AudioSource>();
        ResetPlayerProgress();   // RESET PROGRESS SAAT START

        UpdateUI();
    }

    public void ResetPlayerProgress()
    {
        player.level = 1;
        player.xp = 0;
        player.xpMax = 100;
        player.money = 0;

        player.inventory.Clear();

        Debug.Log("PlayerData telah di-reset!");
    }

    //TAMBAH XP
    public void AddXP(int amount)
    {
        if (player == null)
        {
            Debug.LogError("PLAYER DATA NULL di AddXP()");
            return;
        }

        player.xp += amount;

        // Cek naik level
        if (player.xp >= player.xpMax)
        {
            player.xp = player.xpMax;
            player.level++;

            if (levelUpSFX != null)
                audioSource.PlayOneShot(levelUpSFX);

            Debug.Log("LEVEL UP! Level sekarang: " + player.level);

            if (padlockMarket != null)
                padlockMarket.TryUnlock(player.level);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        levelText.text = "Level " + player.level;
        xpSlider.maxValue = player.xpMax;
        xpSlider.value = player.xp;
        xpText.text = player.xp + "/" + player.xpMax;
    }


}
