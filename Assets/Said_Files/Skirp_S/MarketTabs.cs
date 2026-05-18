using UnityEngine;

public class MarketTabs : MonoBehaviour
{
    public GameObject panelSeeds;
    public GameObject panelFruits;
    public GameObject panelTools;

    public void ShowSeeds()
    {
        panelSeeds.SetActive(true);
        panelFruits.SetActive(false);
        panelTools.SetActive(false);
    }

    public void ShowFruits()
    {
        panelSeeds.SetActive(false);
        panelFruits.SetActive(true);
        panelTools.SetActive(false);
    }

    public void ShowTools()
    {
        panelSeeds.SetActive(false);
        panelFruits.SetActive(false);
        panelTools.SetActive(true);
    }
}
