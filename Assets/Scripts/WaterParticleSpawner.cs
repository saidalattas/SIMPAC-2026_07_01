using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleSpawner : MonoBehaviour
{
    public ParticleSystem waterParticlePrefab;
    public Transform spawnPoint;

    private ParticleSystem currentParticleSystem;

    public void StartWatering()
    {
        if(waterParticlePrefab != null && spawnPoint != null)
        {
            currentParticleSystem = Instantiate(waterParticlePrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            currentParticleSystem.Play();
        }
        else
        {
            Debug.LogWarning("WaterPrefab or Spawnpoint null");
        }
    }

    public void StopWatering()
    {
        if(currentParticleSystem != null)
        {
            currentParticleSystem.Stop();

            Destroy(currentParticleSystem.gameObject, currentParticleSystem.main.startLifetime.constantMax);
            currentParticleSystem = null;
        }
    }


}
