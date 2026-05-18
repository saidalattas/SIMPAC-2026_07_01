using UnityEngine;
using TMPro;

public class BubbleText : MonoBehaviour
{
    public static BubbleText Instance;             // singleton sederhana
    public GameObject bubblePrefab;                // assign BubbleTextPrefab
    public Transform defaultAnchor;                // mis. head transform kasir
    public float defaultDistance = 0.0f;           // offset lokal di atas kepala

    void Awake() => Instance = this;

    // Panggil dari mana saja: BubbleText.Show("teks", anchorOptional, 1.5f);
    public static void Show(string msg, Transform anchor = null, float duration = 1.5f)
    {
        if (Instance == null) { Debug.Log(msg); return; }
        Instance.Spawn(msg, anchor, duration);
    }

    void Spawn(string msg, Transform anchor, float duration)
    {
        var parent = anchor != null ? anchor : defaultAnchor;
        if (parent == null) parent = transform;

        var go = Instantiate(bubblePrefab, parent);
        go.transform.localPosition = Vector3.up * defaultDistance;
        go.SetActive(true);

        var label = go.GetComponentInChildren<TMP_Text>();
        if (label != null) label.text = msg;

        // Biar selalu menghadap kamera
        go.AddComponent<BillboardToCamera>();

        Destroy(go, duration);
    }
}

// Tambahkan script untuk arah camera VR
public class BillboardToCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
