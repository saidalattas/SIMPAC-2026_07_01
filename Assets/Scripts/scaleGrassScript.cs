using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleGrassScript : MonoBehaviour
{
    public GameObject crop;
    public Vector3 scaleIncrement = new Vector3(0.2f, 0.2f, 0.2f); // Pertumbuhan per tetes air
    public Vector3 maxScale = new Vector3(2f, 2f, 2f); // Ukuran maksimum Grass
    public bool isGrowing = false;
    public float growthFactor = 1.25f;
    public Vector3 originalScale;

    private GameObject spawnPoint;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    private void OnCollisionEnter(Collision collision)
    {

        //Jika Grass sudah ada dan terkena air
        if (collision.gameObject.CompareTag("WaterDroplet"))
        {
            //Debug.Log("Grass terkena air nih");
            if(crop != null)
            {
                //Debug.Log("Grow plant");
                spawnCrop();
            }else
            {
                Debug.Log("Plant not set");
            }
        }
        else
        {
            Debug.Log("Rumput terkena object dengan tag: "+collision.gameObject.tag);
        }
    }

 /*   private System.Collections.IEnumerator GrowCornPlant()
    {
        isGrowing = true;
        Vector3 startScale = transform.localScale;
        Vector3 endScale;
        endScale = startScale * growthFactor;
        
        // Limit the maximum scale
        if (endScale.magnitude > originalScale.magnitude * 2f)
        {
            endScale = originalScale * 2f;
            Debug.Log("Spawning Crop");
            spawnCrop();
        }

        float elapsedTime = 0f;

        while (elapsedTime < 2.0f)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / 2.0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
        isGrowing = false;
    }*/

    private void spawnCrop()
    {
        //get current position
        Vector3 position = gameObject.transform.localPosition;
        Quaternion rotation = gameObject.transform.localRotation;
        //instantiate
        GameObject plantCrop =  Instantiate(crop, position, rotation);
        PlantGrowth plantScript = plantCrop.GetComponent<PlantGrowth>();
        plantScript.SetSpawnPointReference(spawnPoint);
        //destroy current
        Destroy(gameObject);
    }

    public void SetCubeReferencePoint(GameObject point)
    {
        spawnPoint = point;
    }
}
