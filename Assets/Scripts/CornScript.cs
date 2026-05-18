using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornScript : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        // Check if the collider is the collecting area
        if (other.gameObject.CompareTag("CollectingArea"))
        {
            Debug.Log("object collide");
            CollectCorn();
        }
    }

     void CollectCorn()
    {
        // Call the method to manage the collected corn
        //InventoryManager.AddCorn();
 
    }
}
