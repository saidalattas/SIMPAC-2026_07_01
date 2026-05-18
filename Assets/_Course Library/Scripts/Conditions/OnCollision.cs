using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Calls functionality when a collision occurs
/// </summary>
public class OnCollision : MonoBehaviour
{
    public float growthFactor = 1.25f; // Factor by which to increase size
    public float growthDuration = 1.0f; // Duration of growth animation in seconds
    public float maxScale = 3.0f; // Maximum scale factor
    public bool isFertilized = false;
    public GameObject matureCornPrefab;


    private Vector3 originalScale;
    private bool isGrowing = false;
    [Serializable] public class CollisionEvent : UnityEvent<Collision> { }

    private void Start()
    {
        originalScale = transform.localScale * 0.5f;
    }
    // When the object enters a collision
    public CollisionEvent OnEnter = new CollisionEvent();

    // When the object exits a collision
    public CollisionEvent OnExit = new CollisionEvent();

    private void OnCollisionEnter(Collision collision)
    {
        //ini kalau siram dan isgrowing
        if (collision.gameObject.CompareTag("WaterDroplet") && !isGrowing)
        {
            Debug.Log("tumbuh cok tumbuh");
            StartCoroutine(GrowCornPlant());
            
        }
        //OnEnter.Invoke(collision);
    }

/*    private void OnCollisionExit(Collision collision)
    {
        OnExit.Invoke(collision);
    }

    private void OnValidate()
    {
        if (TryGetComponent(out Collider collider))
            collider.isTrigger = false;
    }*/

    private System.Collections.IEnumerator GrowCornPlant()
    {
        isGrowing = true;
        Vector3 startScale = transform.localScale;
        Vector3 endScale;
        endScale = startScale * growthFactor;
        
        
        // Limit the maximum scale
        if (endScale.magnitude > originalScale.magnitude * maxScale)
        {
            endScale = originalScale * maxScale;
            ReplaceCorn();
        }

        float elapsedTime = 0f;

        while (elapsedTime < growthDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / growthDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
        isGrowing = false;
    }

    private void ReplaceCorn()
    {
        // Store the current position and rotation
        Vector3 position = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.rotation;

        // Instantiate the mature corn prefab at the same position and rotation
        GameObject matureCorn = Instantiate(matureCornPrefab, position, rotation);

        // Destroy the current corn plant
        Destroy(gameObject);
    }
}
