using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHighlighter : MonoBehaviour
{
    private Outline cropOutline;
    private GameObject currGameObject;

    //private void Start()
    //{
    //    cropOutline = GetComponent<Outline>();
    //    if (cropOutline == null)
    //    {
    //        cropOutline = currGameObject.AddComponent<Outline>();
    //        cropOutline.OutlineColor = Color.magenta;
    //        cropOutline.OutlineWidth = 7.0f;
    //    }

    //    cropOutline.enabled = false;
    //}

    public void setGameObjectReference(GameObject gameObj)
    {
        currGameObject = gameObj;
    }

    public void ActivateHighlight()
    {
        cropOutline = currGameObject.GetComponent<Outline>();
        if (cropOutline == null)
        {
            cropOutline = currGameObject.AddComponent<Outline>();
            cropOutline.OutlineColor = Color.yellow;
            cropOutline.OutlineWidth = 10.0f;
        }

        if (!cropOutline.enabled)
        {
            cropOutline.enabled = true;
            Debug.Log("Highlight activated for " + currGameObject.name);
        }

    }

    public void DeactivateHighlight()
    {
        if (cropOutline.enabled)
        {
            cropOutline.enabled = false;
            Debug.Log("Highlight deactivated for " + currGameObject.name);
        }
    }

}
