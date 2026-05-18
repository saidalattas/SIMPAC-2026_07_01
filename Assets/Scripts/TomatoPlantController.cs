using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoPlantController : MonoBehaviour
{
    public GameObject wateringCan;
    public GameObject fertilizer;
    private int waterCount = 0;
    private bool fertilizerRequired = false;
    private bool isFertilized = false;
    private Vector3 initialScale;
    private Vector3 growthScale;

    // Scale increments for each growth stage
    public Vector3 growthIncrement = new Vector3(0.1f, 0.1f, 0.1f);

    void Start()
    {
        initialScale = transform.localScale;
        growthScale = initialScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name== "WateringCan")
        {
            Debug.Log("check collision");
            /*waterCount++;
            Destroy(other.gameObject); // Menghapus tetesan air setelah menyentuh tanaman

            if (waterCount == 20)
            {
                fertilizerRequired = true;
                Debug.Log("Fertilizer is required to continue growing.");
            }
            else if (waterCount > 20 && isFertilized)
            {
                waterCount = 0; // Reset counter for next phase
                isFertilized = false;
                fertilizerRequired = true;
                Debug.Log("Fertilizer is required to continue growing.");
            }

            // Tumbuhkan tanaman setelah 20 tetes air
            if (waterCount <= 20 && !fertilizerRequired)
            {
                growthScale += growthIncrement;
                transform.localScale = growthScale;
            }*/
        }
       /* else if (other.gameObject == fertilizer && fertilizerRequired)
        {
            isFertilized = true;
            fertilizerRequired = false;
            Debug.Log("Fertilizer applied. You can continue watering the plant.");
            Destroy(fertilizer); // Menghapus objek pupuk setelah digunakan
        }*/
    }
}
