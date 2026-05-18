using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlantGrowth : MonoBehaviour
{
    [Header("Growth Settings")]
    public float maxScale = 1.4f;
    public float growthDuration = 3f;
    public int requiredWaterCount;
    public GameObject fruit;

    [Header("Highlighter")]
    public OutlineHighlighter outlineHighlighter; 

    [Header("Growth State")]
    private Vector3 initialScale;
    private Vector3 targetMaxScale;
    private Vector3 currentScale;
    private int currentWaterCount = 0;
    private bool isGrowing = false;
    private Vector3 targetScale;
    private float growthTimer = 0f;
    private Vector3 initialScaleThisStage;
    private bool isFullyGrown = false;
    private bool isFirstWater = true; // Penanda siraman pertama

    private GameObject spawnPoint;

    private XRGrabInteractable grabInteractable;

    [Obsolete]
    void Start()
    {
        //set value of growth tergantung tanaman.
        //tomat -> 60 - 70 hari. Mid = 65, +- = 6.5, => 7
        //jagung -> 66 - 75 hari. Mid = 71, +- = 7, => 8 (biar beda)
        //pepper -> 60 - 75 hari. Mid = 67.5 +- = 6.75, => 7

        if (gameObject.CompareTag("TomatoPlant"))
        {
            requiredWaterCount = 7;
        }
        else if (gameObject.CompareTag("CornPlant"))
        {
            requiredWaterCount = 8;
        }
        else if (gameObject.CompareTag("PepperPlant"))
        {
            requiredWaterCount = 7;
        }
        else if (gameObject.CompareTag("CabbagePlant"))
        {
            requiredWaterCount = 6;
        }
        else if (gameObject.CompareTag("CarrotPlant"))
        {
            requiredWaterCount = 5;
        }
        // Simpan scale awal dari prefab
        initialScale = transform.localScale;
        // Hitung target scale maksimal
        targetMaxScale = initialScale * maxScale;
        // Set current scale ke scale awal
        currentScale = initialScale;

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable not found!");
        }
    }

    [Obsolete]
    private void OnDestroy()
    {
        if(grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }

    [Obsolete]
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (isFullyGrown)
        {
            SpawnFruit(args.interactorObject.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collider Called");
        if (collision.gameObject.CompareTag("WaterDroplet"))
        {
            //Debug.Log("Water collide");
            if (isFullyGrown)
            {
                Debug.Log("Spawn taneman");
                //SpawnFruit();
                //OutlineHighlighter outline = outlineHighlighter.GetComponent<OutlineHighlighter>();

                outlineHighlighter.setGameObjectReference(gameObject);
                outlineHighlighter.ActivateHighlight();
            }
            else if (isFirstWater)
            {
                    // Siraman pertama hanya mengubah status
                    isFirstWater = false;
                    Debug.Log("Siraman pertama - Tanaman mulai siap tumbuh!");

            }
            else if (currentWaterCount < requiredWaterCount && !isGrowing)
            {
                Debug.Log("Terkena Air");
                currentWaterCount++;
                Debug.Log("Water count saat ini: " + currentWaterCount +", requirednya: "+requiredWaterCount);
                StartGrowthStage();
            }

        }
    }

    //new
    [Obsolete]
    private void SpawnFruit(Transform handTransform)
    {
        if(fruit != null)
        {
            GameObject spawnedFruit = Instantiate(fruit, handTransform.position, handTransform.rotation);
            XRGrabInteractable fruitInteractable = spawnedFruit.GetComponent<XRGrabInteractable>();

            if(fruitInteractable == null)
            {
                fruitInteractable = spawnedFruit.AddComponent<XRGrabInteractable>();
            }


            XRBaseInteractor interactor = handTransform.GetComponent<XRBaseInteractor>();
            if(interactor != null)
            {
                interactor.interactionManager.SelectEnter(interactor, fruitInteractable);
            }

            //spawnedFruit.transform.SetParent(handTransform);
            replaceCubeScript spawnScript = spawnPoint.GetComponent<replaceCubeScript>();
            spawnScript.setState();

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Fruit Prefab belum di-assign!");
        }
    }

    //old
    //private void SpawnFruit()
    //{
    //    // Spawn Corn di posisi tanaman
    //    if (fruit != null)
    //    {
    //        GameObject corn = Instantiate(fruit, transform.position, transform.rotation);
    //        replaceCubeScript spawnScript = spawnPoint.GetComponent<replaceCubeScript>();
    //        spawnScript.setState();
    //        // Hapus tanaman ini
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Buah Prefab belum di-assign!");
    //    }
    //}

    void StartGrowthStage()
    {
        isGrowing = true;
        growthTimer = 0f;
        initialScaleThisStage = currentScale;

        // Hitung target scale untuk tahap ini
        float progressPerStage = 1f / requiredWaterCount;
        float targetProgress = progressPerStage * currentWaterCount;
        targetScale = Vector3.Lerp(initialScale, targetMaxScale, targetProgress);
    }

    void Update()
    {
        if (isGrowing)
        {
            growthTimer += Time.deltaTime;
            float growthProgress = growthTimer / growthDuration;

            if (growthProgress <= 1f)
            {
                // Lerp untuk pertumbuhan yang smooth
                currentScale = Vector3.Lerp(initialScaleThisStage, targetScale, growthProgress);
                transform.localScale = currentScale;
            }
            else
            {
                // Selesai tumbuh untuk tahap ini
                currentScale = targetScale;
                transform.localScale = currentScale;
                isGrowing = false;
                // Cek apakah sudah mencapai pertumbuhan maksimal
                if (currentWaterCount >= requiredWaterCount)
                {
                    if(grabInteractable != null)
                    {
                        grabInteractable.enabled = true;
                    }
                    isFullyGrown = true;
                }
            }
        }
    }

    public void SetSpawnPointReference(GameObject point)
    {
        //Debug.Log("Reference Plant Called");
        spawnPoint = point;
    }
}
