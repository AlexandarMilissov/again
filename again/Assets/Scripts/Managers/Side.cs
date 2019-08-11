using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Side : MonoBehaviour
{
    public string sideName;
    public int turn;
    public Color color;
    List<Unit> units = new List<Unit>();
    [SerializeField]
    public GameObject selectedToAdd = null;

    public void AddTheNewUnit(Tile tile)
    {
        if(tile.owner != this)
        {
            return;
        }



        GameObject go = Instantiate(selectedToAdd, tile.transform.position, Quaternion.identity, tile.transform);
        Unit u = go.GetComponent<Unit>();
        go.transform.Find("Marker").GetComponent<Renderer>().material.color = color;
        
        u.Side = this;
        u.Tile = tile;

        units.Add(u);

        tile.AddUnit(u);
        selectedToAdd = null;
    }


    //user wants to build new unit
    public void RequestUnit(GameObject unit)
    {
        //check if he already has a selected unit
        //if he doesnt - add the new selected
        if(selectedToAdd == null)
        {
            selectedToAdd = unit;
        }
        //else if he has a selected unit
        else
        {
            //and if this unit is of the same type
            //deselect the unit
            if(selectedToAdd.GetType().Equals(unit.GetType()))
            {
                selectedToAdd = null;
            }
            //else change to the new unit
            else
            {
                selectedToAdd = unit;
            }
        }
    }
}
