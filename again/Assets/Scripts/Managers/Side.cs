using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Side : MonoBehaviour
{
    public string name;
    public int turn;
    List<Unit> units = new List<Unit>();
    [SerializeField]
    public GameObject selectedToAdd = null;
    
    public void AddTheNewUnit(Tile tile)
    {
        units.Add(selectedToAdd.GetComponent<Unit>());
        Instantiate(selectedToAdd, new Vector3(0, 0, 0), Quaternion.identity, tile.transform);
        tile.AddUnit(selectedToAdd.GetComponent<Unit>());
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
