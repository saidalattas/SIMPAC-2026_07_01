using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyColliderController : MonoBehaviour
{
    public GameObject grassPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlantPlaceholder"))
        {
            Debug.Log("TomatBibit telah menyentuh placeholder.");
           // Vector3 position = gameObject.transform.position;
           // Quaternion rotation = gameObject.transform.rotation;

           // GameObject grass = Instantiate(grassPrefab, position, rotation);

            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("SeedBag") || collision.gameObject.CompareTag("WaterCan"))
        {
            Debug.Log("kena seedbag / watercan");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
