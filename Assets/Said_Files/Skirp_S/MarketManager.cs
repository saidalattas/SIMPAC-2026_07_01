using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public PlayerData player;
    public AudioSource audioSource;
    public AudioClip buySFX, sellSFX;
    public Transform bubbleAnchor;

    // daftar harga jual & beli
    public Dictionary<string, int> buyPrice = new Dictionary<string, int>();
    public Dictionary<string, int> sellPrice = new Dictionary<string, int>();

    // LIMIT STOK HARIAN 
    public Dictionary<string, int> stockLimit = new Dictionary<string, int>();

    void Start()
    {

        //  SEEDS (biji) 
        buyPrice.Add("Seed_Carrot", 1500);
        buyPrice.Add("Seed_Cabbage", 1800);
        buyPrice.Add("Seed_Pepper", 2000);
        buyPrice.Add("Seed_Corn", 1700);
        buyPrice.Add("Seed_Tomato", 1600);

        // LIMIT STOCK UNTUK SEEDS 
        stockLimit.Add("Seed_Carrot", 5);
        stockLimit.Add("Seed_Cabbage", 5);
        stockLimit.Add("Seed_Pepper", 5);
        stockLimit.Add("Seed_Corn", 5);
        stockLimit.Add("Seed_Tomato", 5);

        //  FRUITS (hasil panen) 
        sellPrice.Add("Carrot", 8000);
        sellPrice.Add("Cabbage", 6000);
        sellPrice.Add("Pepper", 3000);
        sellPrice.Add("Corn", 4000);
        sellPrice.Add("Tomato", 5000);

        //  TOOLS 
        buyPrice.Add("Tool_Shovel", 10000);
        buyPrice.Add("Tool_Rake", 12000);
        buyPrice.Add("Tool_Pitchfork", 15000);

        // Pilihan : batasi tools
        stockLimit.Add("Tool_Shovel", 1);
        stockLimit.Add("Tool_Rake", 1);
        stockLimit.Add("Tool_Pitchfork", 1);

        ResetAll();
    }

    //BELI 
    public void BuyItem(string itemId)
    {
        if (!buyPrice.ContainsKey(itemId))
        {
            Debug.LogWarning("Item tidak tersedia untuk dibeli: " + itemId);
            return;
        }

        //CEK STOK
        if (stockLimit.ContainsKey(itemId) && stockLimit[itemId] <= 0)
        {
            BubbleText.Show(
                "Oh no! We're out of stock!\nCome back tomorrow!",
                bubbleAnchor,
                1.5f
            );
            Debug.Log("Stock habis untuk " + itemId);
            return;
        }

        int price = buyPrice[itemId];

        if (player.SpendMoney(price))
        {
            // if(player.AddItem(...)) karena AddItem itu void
            player.AddItem(itemId, 1);
            audioSource.PlayOneShot(buySFX);

            // Kurangi stok
            if (stockLimit.ContainsKey(itemId))
                stockLimit[itemId]--;

            //UPDATE UI STOCK
            foreach (var btn in FindObjectsOfType<MarketButtonPrice>())
            {
                btn.UpdateUI();
            }

            BubbleText.Show("Purchase success!", bubbleAnchor, 1.5f);
            Debug.Log($"Berhasil membeli {itemId} (Sisa stok: {stockLimit[itemId]})");
        }
        else
        {
            BubbleText.Show("Uang tidak cukup!", bubbleAnchor, 1.5f);
            Debug.Log("Uang tidak cukup!");
        }
    }

    //JUAL 
    public void SellItem(string itemId)
    {
        if (!sellPrice.ContainsKey(itemId))
        {
            Debug.LogWarning("Item tidak bisa dijual: " + itemId);
            return;
        }

        //  RemoveItem memang mengembalikan bool 
        if (player.RemoveItem(itemId, 1))
        {
            player.AddMoney(sellPrice[itemId]);
            audioSource.PlayOneShot(sellSFX);
            Debug.Log($"Berhasil menjual {itemId}");

            BubbleText.Show("Berhasil menjual " + itemId + "!", bubbleAnchor, 1.5f);
        }
        else
        {
            Debug.Log($"Tidak ada {itemId} di inventory untuk dijual!");
            BubbleText.Show("Tidak ada " + itemId + " di inventory!", bubbleAnchor, 1.5f);
        }
    }

    public void ResetAll()
    {
        stockLimit["Seed_Carrot"] = 5;
        stockLimit["Seed_Cabbage"] = 5;
        stockLimit["Seed_Pepper"] = 5;
        stockLimit["Seed_Corn"] = 5;
        stockLimit["Seed_Tomato"] = 5;

        // Refresh UI
        foreach (var btn in FindObjectsOfType<MarketButtonPrice>())
            btn.UpdateUI();
    }
}
