using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text xpText;
    public TMP_Text itemListText;

    public PlayerData player;  // drag PlayerData.asset 

    private void OnEnable()
    {
        if (player != null)
            player.OnDataChanged += UpdateUI;
    }

    private void OnDisable()
    {
        if (player != null)
            player.OnDataChanged -= UpdateUI;
    }

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("InventoryUI: PLAYER DATA BELUM DIISI!");
            return;
        }

        // Registrasi ulang supaya 100%
        player.OnDataChanged -= UpdateUI;
        player.OnDataChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyText.text = "Rp " + player.money.ToString("N0");
        xpText.text = "XP " + player.xp + "/" + player.xpMax;

        itemListText.text = "";
        foreach (var i in player.inventory)
            itemListText.text += $"{i.Key}: {i.Value}\n";
    }
}
