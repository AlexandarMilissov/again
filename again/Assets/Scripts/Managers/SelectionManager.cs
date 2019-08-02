using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    float ROTSpeed = 10f;
    private Vector3 dragOrigin;

    public string selectableTag = "Selectable";
    public GameObject selectedObject;
    public Color selectionColor = Color.yellow;
    private Color selectedObjectDefaultColor;
    
    public Side currentSide;



    //left click
    public int selectionKey = 0;
    //right click
    public int actionKey = 1;
    //scroll key
    public int dragKey = 2;

    void Update()
    {


        GameObject pointedGameObject = GetPointedGameObject();
        if (Input.GetMouseButtonDown(selectionKey))
        {
            Select(pointedGameObject);
        }
        else if (Input.GetMouseButtonDown(actionKey))
        {
            DoAction(pointedGameObject);
        }
        else if(Input.GetMouseButtonDown(dragKey))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(dragKey))
        {
            return;
        }



        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x, 0, -pos.y);
        Camera.main.transform.Translate(move, Space.World);
        Camera.main.transform.Translate(0,0, Input.GetAxis("Mouse ScrollWheel") * ROTSpeed);

    }

    GameObject GetPointedGameObject()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.parent.gameObject;
        }
        else
        {
            return null;
        }
    }

    void Select(GameObject toSelect)
    {
        //check if we dont have object to select
        if(toSelect == null)
        {
            return;
        }
        if(!toSelect.CompareTag(selectableTag))
        {
            toSelect = FindParentWithTag(toSelect, selectableTag);
        }
        
        if (toSelect == null)
        {
            return;
        }

        //check if object is selectable
        if (toSelect.tag == selectableTag)
        { 
            //if we dont have selected object we select it 
            if (selectedObject == null)
            {
                //select object
                selectedObject = toSelect;
                //get its default color
                selectedObjectDefaultColor = selectedObject.GetComponentInChildren<Renderer>().material.color;
                //change it to selection color
                selectedObject.GetComponentInChildren<Renderer>().material.color = selectionColor;
            }
            //else if the new selected object is not the old selected object
            //we select the new object
            else if (selectedObject != toSelect)
            {
                //we return its color
                selectedObject.GetComponentInChildren<Renderer>().material.color = selectedObjectDefaultColor;

                //select the new object
                selectedObject = toSelect;
                //get its default color
                selectedObjectDefaultColor = selectedObject.GetComponentInChildren<Renderer>().material.color;
                //change it to selection color
                selectedObject.GetComponentInChildren<Renderer>().material.color = selectionColor;
            }
            //if we press the already selected object we deselect it
            else
            {
                //we return its color
                selectedObject.GetComponentInChildren<Renderer>().material.color = selectedObjectDefaultColor;
                //and deselect it
                selectedObject = null;
            }

        }
    }

    private GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        if(childObject == null)
        {
            return null;
        }
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    void DoAction(GameObject todoAction)
    {
        if(currentSide.selectedToAdd != null)
        {
            if(todoAction.GetComponent<Tile>() != null 
                && todoAction.GetComponent<Tile>().unit == null)
            {
                currentSide.AddTheNewUnit(todoAction.GetComponent<Tile>());
            }
        }
    }

}
