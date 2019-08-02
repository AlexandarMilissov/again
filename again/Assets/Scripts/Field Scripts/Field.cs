using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject TilePrefab;
    public static int sizeX = 10;
    public static int sizeZ = 10;
    public float offset = 1.1f;
    public Side left, right;
    
    public Tile[,] field = new Tile[sizeX, sizeZ];
    public GameObject unit;

    void Start()
    {
        CreateField();



        GameObject Unit = Instantiate(unit, new Vector3(0, 0, 0), Quaternion.identity, field[0, 0].transform);
        Unit.GetComponent<Unit>().Tile = field[0, 0];
        field[0, 0].unit = Unit.GetComponent<Unit>();
    }
    
    private void CreateField()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                GameObject currentTile = Instantiate(TilePrefab, new Vector3(x * offset, 0, z * offset), Quaternion.identity, this.transform);
                currentTile.name = "(" + x + ", " + z + ")";
                field[x, z] = currentTile.GetComponent<Tile>();
            }
        }
    }
}
