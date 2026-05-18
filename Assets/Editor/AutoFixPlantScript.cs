using UnityEngine;
using UnityEditor;
using System.IO;

public class AutoFixPlantScript : EditorWindow
{
    private string[] plantKeywords =
    {
        "Carrot", "Tomato", "Pepper", "Cabbage", "Corn",
        "Plant", "Seed", "LOD0", "LOD1"
    };

    [MenuItem("Tools/Auto Fix/Auto Fix All Plants")]
    public static void ShowWindow()
    {
        AutoFixPlantScript window = GetWindow<AutoFixPlantScript>("Auto Fix Plants");
        window.minSize = new Vector2(350, 150);
    }

    private void OnGUI()
    {
        GUILayout.Label("Auto Fix Plant Meshes", EditorStyles.boldLabel);
        GUILayout.Label("Perbaiki semua tanaman otomatis:", EditorStyles.label);

        if (GUILayout.Button(" Jalankan Auto-Fix Sekarang"))
        {
            FixAllPlants();
        }
    }

    private void FixAllPlants()
    {
        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        int fixedCount = 0;

        foreach (string assetPath in allAssets)
        {
            if (!assetPath.EndsWith(".fbx") && !assetPath.EndsWith(".prefab"))
                continue;

            if (!ContainsPlantKeyword(assetPath))
                continue;

            Object obj = AssetDatabase.LoadMainAssetAtPath(assetPath);
            if (obj == null) continue;

           
            if (assetPath.EndsWith(".fbx"))
            {
                ModelImporter importer = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                if (importer != null && !importer.isReadable)
                {
                    importer.isReadable = true;
                    importer.SaveAndReimport();
                    Debug.Log($"[AutoFix] Enabled Read/Write{assetPath}");
                    fixedCount++;
                }
            }

            
            if (assetPath.EndsWith(".prefab"))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (prefab == null) continue;

                FixPrefab(prefab);
                fixedCount++;
            }
        }

        Debug.Log($"[AutoFix] Selesai! Total asset diperbaiki: {fixedCount}");
    }

    private void FixPrefab(GameObject prefab)
    {
        bool changed = false;

        
        MeshCollider meshCol = prefab.GetComponent<MeshCollider>();
        Rigidbody rb = prefab.GetComponent<Rigidbody>();

        if (meshCol != null)
        {
            
            if (!meshCol.convex)
            {
                meshCol.convex = true;
                Debug.Log($"[AutoFix] Convex enabled  {prefab.name}");
                changed = true;
            }
        }

        
        if (!prefab.GetComponent<Collider>())
        {
            prefab.AddComponent<BoxCollider>();
            Debug.Log($"[AutoFix] BoxCollider ditambahkan  {prefab.name}");
            changed = true;
        }

        
        if (rb != null)
        {
            Object.DestroyImmediate(rb, true);
            Debug.Log($"[AutoFix] Rigidbody dihapus  {prefab.name}");
            changed = true;
        }

        if (changed)
        {
            PrefabUtility.SavePrefabAsset(prefab);
        }
    }

    private bool ContainsPlantKeyword(string path)
    {
        foreach (string word in plantKeywords)
        {
            if (path.ToLower().Contains(word.ToLower()))
                return true;
        }
        return false;
    }
}
