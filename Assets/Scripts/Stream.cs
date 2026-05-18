using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private Vector3 targetPosition = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

   
    // Start is called before the first frame update
    void Start()
    {
        MovePosition(0, transform.position);
        MovePosition(1, transform.position);
        // Change the scale of the stream to make it larger (like pouring water)
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); // Adjust this to make the stream larger

        // Change the width of the stream to make it larger
        lineRenderer.startWidth = 0.1f; // Make stream wider
        lineRenderer.endWidth = 0.1f;
        // Change the color of the stream to dark blue
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use appropriate shader
        lineRenderer.startColor = new Color(0.4f, 0.7f, 1.0f); // Light Blue (Water Color)
        lineRenderer.endColor = new Color(0.4f, 0.7f, 1.0f);   // Light Blue (Water Color)
    }

    public void Begin()
    {
        Debug.Log("Begin pouring");     
        StartCoroutine(BeginPour());
    }

    public void End()
    {
        StopCoroutine(EndPour());
    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MovePosition(0, transform.position);
            MovePosition(1, targetPosition);
            yield return null;
        }
      
    }

    private IEnumerator EndPour()
    {
        Destroy(gameObject);
        yield return null;
    }
   // private Vector3 FindEndPoint()
   // {
     //   RaycastHit hit;
    //    Ray ray = new Ray(transform.position, Vector3.down);

     //   Physics.Raycast(ray, out hit, 2.0f);
     //   Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
     //   return endPoint;
    //}
    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            return hit.point;
        }
        else
        {
            Debug.Log("No hit detected.");
            return ray.GetPoint(2.0f); // Use default distance
        }
    }


    private void MovePosition(int index, Vector3 targetPosition)
    {
        Debug.Log($"Setting position {index} to {targetPosition}");
        lineRenderer.SetPosition(index, targetPosition);
    }
}
