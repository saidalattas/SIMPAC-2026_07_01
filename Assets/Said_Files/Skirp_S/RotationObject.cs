
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    float rotationSpeed = 30f;


    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

}
