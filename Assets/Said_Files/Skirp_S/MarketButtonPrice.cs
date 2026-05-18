using TMPro;
using UnityEngine;

public class MarketButtonPrice : MonoBehaviour
{
    public string itemId;
    public bool isBuyButton;
    public TMP_Text priceText;
    public TMP_Text stockText;   // UI stok

    private MarketManager market;

    void Start()
    {
        market = FindObjectOfType<MarketManager>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (market == null) return;

        //PRICE
        int price = 0;

        if (isBuyButton)
        {
            if (market.buyPrice.ContainsKey(itemId))
                price = market.buyPrice[itemId];
        }
        else
        {
            if (market.sellPrice.ContainsKey(itemId))
                price = market.sellPrice[itemId];
        }

        priceText.text = "Rp " + price.ToString("N0");

        //STOCK (khusus BUY) 
        if (isBuyButton && stockText != null)
        {
            if (market.stockLimit.ContainsKey(itemId))
            {
                int sisa = market.stockLimit[itemId];
                stockText.text = "Stock: " + sisa;

                // warna jika habis
                if (sisa <= 0)
                    stockText.color = Color.red;
                else
                    stockText.color = Color.white;
            }
            else
            {
                stockText.text = "Stock: ";
            }
        }
    }
}
