using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCenterOfMass : MonoBehaviour
{
    public Vector3 centerOfMass;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        rb.centerOfMass = centerOfMass;
        rb.WakeUp();
        #endif
    }

    private void OnDrawGizmos()
    {
        Debug.Log("Gizmo called");
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 0.2f);
    }
}
