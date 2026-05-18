using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class replaceCubeScript : MonoBehaviour
{
    //public GameObject grassPrefab; // Prefab untuk Grass kecil
    public Vector3 initialGrassScale = new Vector3(0.5f, 0.5f, 0.5f); // Skala awal rumput kecil
    private GameObject currentGrass; // Menyimpan objek Grass
    private bool isGrass = false; // Apakah sudah menjadi Grass
    private bool isCollide = false;
    public Dictionary<string,GameObject> grass = new Dictionary<string, GameObject>();
    public GameObject grassCorn, grassTomato, grassCabbage, grassCarrot, grassPepper;
    public Renderer tillageRenderer;
    private bool isActive = false;
    public ParticleSystem dirtParticle;

    private Color visibleColor = new Color(101f / 255f, 74f / 255f, 51f / 255f);
    private Color transparentColor = new Color(1, 1, 1, 0);

    private Material cubeMaterial;

    public void Start()
    {
        //add new components
        grass.Add("CornSeed", grassCorn);
        grass.Add("TomatoSeed", grassTomato);
        grass.Add("CarrotSeed", grassCarrot);
        grass.Add("CabbageSeed", grassCabbage);
        grass.Add("PepperSeed", grassPepper);
        

        cubeMaterial = GetComponent<Renderer>().material;
        SetCubeColor(transparentColor, true);
        if (isActive == false)
        {
            //SetCubeColor(transparentColor, true);
            Debug.Log("tillagerenderer called false");
            tillageRenderer.enabled = false;
        }
        else
        {
            //SetCubeColor(visibleColor, false);
            tillageRenderer.enabled = true;
            dirtParticle.Play();
        }
    }

    private void SetCubeColor(Color color, bool makeTransparent)
    {
        cubeMaterial.color = color;
        if (makeTransparent)
        {
            // Set material to Transparent mode for URP Lit Shader
            cubeMaterial.SetFloat("_Surface", 1); // 1 for transparent
            cubeMaterial.SetOverrideTag("RenderType", "Transparent");
            cubeMaterial.renderQueue = (int)RenderQueue.Transparent;
            cubeMaterial.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            cubeMaterial.DisableKeyword("_SURFACE_TYPE_OPAQUE");
            cubeMaterial.SetFloat("_SrcBlend", (int)BlendMode.SrcAlpha);
            cubeMaterial.SetFloat("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            cubeMaterial.SetFloat("_ZWrite", 0);
        }
        else
        {
            // Set material back to Opaque mode for URP Lit Shader
            cubeMaterial.SetFloat("_Surface", 0); // 0 for opaque
            cubeMaterial.SetOverrideTag("RenderType", "Opaque");
            cubeMaterial.renderQueue = (int)RenderQueue.Geometry;
            cubeMaterial.EnableKeyword("_SURFACE_TYPE_OPAQUE");
            cubeMaterial.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            cubeMaterial.SetFloat("_SrcBlend", (int)BlendMode.One);
            cubeMaterial.SetFloat("_DstBlend", (int)BlendMode.Zero);
            cubeMaterial.SetFloat("_ZWrite", 1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(isCollide);
        // Ketika TomatBibit menyentuh PlantPlaceholder, ganti dengan Grass kecil
        Debug.Log(collision.gameObject.tag+" - "+collision.gameObject.tag.Contains("Seed"));
        if (!isGrass && collision.gameObject.tag.Contains("Seed") && isCollide==false && isActive == true)
        {
            Debug.Log("seed collide with "+collision.gameObject.tag);
            string seedTag = collision.gameObject.tag;
            Vector3 position = gameObject.transform.position; // Ambil posisi objek ini
            Quaternion rotation = gameObject.transform.rotation; // Ambil rotasi objek ini

            GameObject grassPlant = grass[seedTag];

            // Instansiasi objek Grass kecil di tempat TomatBibit
            currentGrass = Instantiate(grassPlant, position, rotation);
            currentGrass.transform.localScale = initialGrassScale; // Set skala awal rumput
            ScaleGrassScript grassScript = currentGrass.GetComponent<ScaleGrassScript>();

            isGrass = true; // Tandai sebagai rumput
            isCollide = true;

            grassScript.SetCubeReferencePoint(gameObject);
            //SetCubeColor(transparentColor, true);
            //Destroy(collision.gameObject); // Hapus PlantPlaceholder
            //Destroy(gameObject); // Hapus objek yang memiliki skrip ini (TomatBibit)
        }

        if (collision.gameObject.tag.Contains("Shovel") && isActive == false)
        {
            //SetCubeColor(visibleColor, false);
            isActive = true;
            tillageRenderer.enabled = true;
            dirtParticle.Play();
        }
    }

    public void setState()
    {
        isActive = false;
        isCollide = false;
        isGrass = false;
        SetCubeColor(transparentColor, true);
        tillageRenderer.enabled = false;
    }
}
