using UnityEngine;
using System.Collections.Generic;

public  class InventoryManager:MonoBehaviour
{
    /*    public static int cornCount = 0;

        public static void AddCorn()
        {
            cornCount++;
            Debug.Log("Corn collected! Total: " + cornCount);
        }*/

    public MechanismManager mechanism;
    private List<string> validTags = new List<string> { "Cabbage", "Corn", "Tomato", "Pepper", "Carrot" };

    private void OnTriggerEnter(Collider other)
    {
        if (validTags.Contains(other.gameObject.tag))
        {
            mechanism.shippingPlant(other.gameObject.tag);
            Debug.Log(other.gameObject.tag + " collected!");
            mechanism.showShippingDetail();
            Destroy(other.gameObject);
        }
    }
}
