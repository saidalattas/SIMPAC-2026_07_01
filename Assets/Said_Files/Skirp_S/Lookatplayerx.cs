using UnityEngine;

public class Lookatplayerx : MonoBehaviour
{
    public Transform player;          // XR Rig target
    public float rotateSpeed = 3f;    // smooth rotation
    public bool onlyY = true;         // rotasi hanya di Y-axis

    void Update()
    {
        if (player == null) return;

        // arah posisi target
        Vector3 direction = player.position - transform.position;

        // jika hanya Y-axis
        if (onlyY)
        {
            direction.y = 0;
        }

        // rotasi yang diinginkan
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // rotasi halus
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
    }
}
