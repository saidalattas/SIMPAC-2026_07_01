using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingManagers
{
    // Start is called before the first frame update
    int shipped;
    string crop;

    public ShippingManagers(string crop)
    {
        this.shipped = 1;
        this.crop = crop;
    }
    // Update is called once per frame
    public void shipCrop()
    {
        this.shipped++;
    }

    public string getCropName()
    {
        return this.crop;
    }

    public int getShippedValue()
    {
        return this.shipped;
    }
}
