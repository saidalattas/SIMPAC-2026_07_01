using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarUI : MonoBehaviour
{
    public Slider xpSlider;
    public TMP_Text xpText;

    public PlayerData player;

    private void Start()
    {
        // player di-assign lewat Inspector
        if (player == null)
        {
            Debug.LogError("PlayerData TIDAK di-assign di XPBarUI!");
            return;
        }

        xpSlider.maxValue = player.xpMax;
        xpSlider.value = player.xp;

        UpdateUI();
    }

    public void UpdateUI()
    {
        xpSlider.maxValue = player.xpMax;
        xpSlider.value = player.xp;

        xpText.text = "XP " + player.xp + "/" + player.xpMax;
    }
}
