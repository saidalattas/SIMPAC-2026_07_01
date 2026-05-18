using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Contains("WaterCan"))
        {
            Destroy(gameObject);
        }
    }
}
