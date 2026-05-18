using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using TMPro;

public class MechanismManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MechanismManager instance;
    List<ShippingManagers> shippingManagers;
    //public List<string> objectiveTag;

    public TextMeshProUGUI cornCounter;
    public TextMeshProUGUI cabbageCounter;
    public TextMeshProUGUI tomatoCounter;
    public TextMeshProUGUI carrotCounter;
    public TextMeshProUGUI pepperCounter;

    public TextMeshProUGUI cornTargetText;
    public TextMeshProUGUI cabbageTargetText;
    public TextMeshProUGUI tomatoTargetText;
    public TextMeshProUGUI carrotTargetText;
    public TextMeshProUGUI pepperTargetText;

    private int cornTarget, cabbageTarget, tomatoTarget, carrotTarget, pepperTarget;
    private int maxCrop = 3;

    private void Awake()
    {
        instance = this;
        this.shippingManagers = new List<ShippingManagers>();
    }

    void Start()
    {
        cornTarget = Random.Range(0, maxCrop);
        cabbageTarget = Random.Range(0, maxCrop);
        tomatoTarget = Random.Range(0, maxCrop);
        carrotTarget = Random.Range(0, maxCrop);
        pepperTarget = Random.Range(0, maxCrop);

        cornTargetText.text = "" + cornTarget;
        cabbageTargetText.text = "" + cabbageTarget;
        tomatoTargetText.text = "" + tomatoTarget;
        carrotTargetText.text = "" + carrotTarget;
        pepperTargetText.text = "" + pepperTarget;
    }

    public void RenewOrder()
    {
        cornTarget = Random.Range(0, maxCrop);
        cabbageTarget = Random.Range(0, maxCrop);
        tomatoTarget = Random.Range(0, maxCrop);
        carrotTarget = Random.Range(0, maxCrop);
        pepperTarget = Random.Range(0, maxCrop);

        cornTargetText.text = "" + cornTarget;
        cabbageTargetText.text = "" + cabbageTarget;
        tomatoTargetText.text = "" + tomatoTarget;
        carrotTargetText.text = "" + carrotTarget;
        pepperTargetText.text = "" + pepperTarget;
    }

    //akan dipanggil saat shipping plant (plant nya masuk ke dalam si kotak)
    public void shippingPlant(string crop)
    {

        if (shippingManagers.Count == 0) { 
            shippingManagers.Add(new ShippingManagers(crop));
        }else
        {
            ShippingManagers plant = shippingManagers.FirstOrDefault(obj => obj.getCropName().Equals(crop));
            if (plant != null) {
                plant.shipCrop();
            }
            else
            {
                shippingManagers.Add(new ShippingManagers(crop));
                Debug.Log("Error");
            }
                
        }
    }

    public void showShippingDetail()
    {
        foreach (ShippingManagers item in shippingManagers)
        {
            Debug.Log("Crop: " + item.getCropName()+", shipped: "+item.getShippedValue());
            if(item.getCropName() == "Corn")
            {
                cornCounter.text = "" + item.getShippedValue();
            }else if(item.getCropName() == "Cabbage")
            {
                cabbageCounter.text = "" + item.getShippedValue();
            }else if(item.getCropName() == "Tomato")
            {
                tomatoCounter.text = "" + item.getShippedValue();
            }else if(item.getCropName() == "Carrot")
            {
                carrotCounter.text = "" + item.getShippedValue();
            }else if(item.getCropName() == "Cabbage")
            {
                cabbageCounter.text = "" + item.getShippedValue();
            }else if(item.getCropName() == "Pepper")
            {
                pepperCounter.text = "" + item.getShippedValue();
            }
        }
    }

}
