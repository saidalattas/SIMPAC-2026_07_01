using UnityEngine;

public class PadlockController : MonoBehaviour
{
    [Header("Level Requirement")]
    public int requiredLevel = 2;   // level yang dibutuhkan untuk membuka area

    [Header("Components")]
    public Animator padlockAnimator;
    public GameObject padlockObject;   // object fisik gembok
    public Collider blockCollider;     // collider yang menghalangi area
    public GameObject unlockEffect;    // efek partikel / UI bubble opsional

    private bool isUnlocked = false;

    // DIPANGGIL OLEH LevelManager

    public void TryUnlock(int currentPlayerLevel)
    {
        if (isUnlocked) return;
        if (currentPlayerLevel < requiredLevel) return;

        // buka gembok
        UnlockPadlock();
    }

    // PROSES BUKA AREA
    private void UnlockPadlock()
    {
        isUnlocked = true;

        Debug.Log("Padlock unlocked!");

        // mainkan animasi buka
        if (padlockAnimator != null)
            padlockAnimator.SetTrigger("Open");

        // nonaktifkan collider penghalang
        if (blockCollider != null)
            blockCollider.enabled = false;

        // hilangkan objek gembok
        if (padlockObject != null)
            Destroy(padlockObject, 0.5f);

        // efek unlock 
        if (unlockEffect != null)
            unlockEffect.SetActive(true);
    }
}
