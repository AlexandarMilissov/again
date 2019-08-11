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
    public Transform middle;


    //left click
    public int selectionKey = 0;
    //right click
    public int actionKey = 1;
    //scroll key
    public int dragKey = 2;

    void Update()
    {


        GameObject pointedGameObject = GetPointedGameObject();
        if (Input.GetMouseButtonUp(selectionKey))
        {
            Select(pointedGameObject);
        }
        else if (Input.GetMouseButtonUp(actionKey))
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
        if(toSelect == null)
        {
            return;
        }
        //place unit if player has selected a new unit
        if (currentSide.selectedToAdd != null)
        {
            if (toSelect.GetComponent<Tile>() != null
                && toSelect.GetComponent<Tile>().unit == null)
            {
                currentSide.AddTheNewUnit(toSelect.GetComponent<Tile>());
                return;
            }
        }



        //check if we dont have object to select
        if (toSelect == null)
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
                Deselect();

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
                Deselect();
            }

        }
    }

    private void Deselect()
    {
        //we return its color
        selectedObject.GetComponentInChildren<Renderer>().material.color = selectedObjectDefaultColor;
        //and deselect it
        selectedObject = null;
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
        if(todoAction == null)
        {
            return;
        }


        //check if we have selected object and is of type tile
        if(selectedObject != null && selectedObject.GetComponent<Tile>() != null)
        {
            //check if there is a unit on our side on it
            if (selectedObject.GetComponent<Tile>().unit != null && selectedObject.GetComponent<Tile>().unit.Side == currentSide)
            {
                //check to see if tile is empty
                if(todoAction.GetComponent<Tile>() != null && todoAction.GetComponent<Tile>().unit == null)
                {
                    Tile newTile = todoAction.GetComponent<Tile>();
                    PathMove(newTile);
                    Deselect();
                }
            }
        }
    }

    Queue<Tile> FindPath(Tile start, Tile end, out float cost)
    {
        cost = 0;

        List<Tile> opened = new List<Tile>();
        HashSet<Tile> closed = new HashSet<Tile>();
        foreach (var n in start.nodes)
        {
            n.endTile.pathFindCostToHere = n.cost;
            n.endTile.pathFindParent = start;
            opened.Add(n.endTile);
        }
        closed.Add(start);

        while (opened.Count != 0)
        {
            int i = opened.Count - 1;
            for (; i >= 0; i--)
            {
                if(opened[i] == end)
                {
                    break;
                }
                closed.Add(opened[i]);
                foreach(var n in opened[i].nodes)
                {
                    if(closed.Contains(n.endTile))
                    {
                        continue;
                    }
                    if(opened.Contains(n.endTile))
                    {
                        if(n.endTile.pathFindCostToHere > opened[i].pathFindCostToHere + n.cost)
                        {
                            n.endTile.pathFindParent = opened[i];
                            n.endTile.pathFindCostToHere = opened[i].pathFindCostToHere + n.cost;
                        }
                    }
                    else
                    {
                        n.endTile.pathFindParent = opened[i];
                        n.endTile.pathFindCostToHere = opened[i].pathFindCostToHere + n.cost;
                        opened.Add(n.endTile);
                    }
                }
                opened.Remove(opened[i]);
            }
            if(end.pathFindParent != null)
            {
                break;
            }
        }
        
        Queue<Tile> path = new Queue<Tile>();
        Tile t = end;
        if(t.pathFindParent == null)
        {
            return null;
        }
        path.Enqueue(t);
        while (t.pathFindParent != null)
        {
            path.Enqueue(t.pathFindParent);
            t = t.pathFindParent;
        }
        cost = end.pathFindCostToHere;

        foreach (var test in opened)
        {
            test.pathFindCostToHere = 0;
            test.pathFindParent = null;
        }
        foreach (var test in closed)
        {
            test.pathFindCostToHere = 0;
            test.pathFindParent = null;
        }
        return path;
    }

    void PathMove(Tile newTile)
    {
        Tile oldTile = selectedObject.GetComponent<Tile>();
        Queue<Tile> path = FindPath(oldTile, newTile, out float cost);
        Debug.Log(cost);

        newTile.transform.Find("Visual").GetComponent<Renderer>().material.color = Color.red;
        oldTile.transform.Find("Visual").GetComponent<Renderer>().material.color = Color.red;
        foreach (var t in path)
        {
            t.transform.Find("Visual").GetComponent<Renderer>().material.color = Color.red;
        }

        MoveSelectedUnit(oldTile,newTile);
    }
    void MoveSelectedUnit(Tile oldTile, Tile newTile)
    {

        Unit u = oldTile.unit;
        u.gameObject.transform.SetParent
            (newTile.transform);
        
        newTile.AddUnit(u);
        oldTile.RemoveUnit();
    }
}
