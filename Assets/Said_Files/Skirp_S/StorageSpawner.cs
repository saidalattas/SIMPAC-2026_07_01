using UnityEngine;

public class SimpleStorageSpawner : MonoBehaviour
{
    public Transform storagePoint;
    public GameObject itemPrefab;

    private bool alreadySpawned = false;

    public void SpawnItem()
    {
        if (alreadySpawned) return;

        Instantiate(itemPrefab, storagePoint.position, storagePoint.rotation);
        alreadySpawned = true;
    }
}