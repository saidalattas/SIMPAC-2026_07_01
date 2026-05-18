using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    public Transform controllerTransform;
    public float rayLength = 10f;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    public UnityEngine.InputSystem.InputAction selectAction;

    void OnEnable()
    {
        // Enable the Input Action
        if (selectAction != null)
        {
            selectAction.Enable();
        }
    }

    void OnDisable()
    {
        // Disable the Input Action
        if (selectAction != null)
        {
            selectAction.Disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);

        //is mouse point object
        if (Physics.Raycast(ray, out raycastHit, rayLength)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;

            //if object tag "Selectable" & selection is not highlighted yet
            if (highlight.CompareTag("Meteor") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (selectAction != null && selectAction.WasPerformedThisFrame())
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }
}
