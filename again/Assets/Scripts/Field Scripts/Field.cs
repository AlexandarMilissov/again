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
        
    }
    
    private void CreateField()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                GameObject currentTile = Instantiate(TilePrefab, new Vector3(x * offset, 0, z * offset), Quaternion.identity, this.transform);
                currentTile.GetComponent<Tile>().posX = x;
                currentTile.GetComponent<Tile>().posZ = z;
                currentTile.GetComponent<Tile>().nodes = new List<Node>();
                currentTile.name = "(" + x + ", " + z + ")";
                field[x, z] = currentTile.GetComponent<Tile>();
                
            }
        }
        for (int x = 0; x < sizeX/5; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                field[x, z].owner = left;
                field[x, z].transform.Find("Visual").GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        for (int x = (int)(sizeX * 0.8); x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                field[x, z].owner = right;
                field[x, z].transform.Find("Visual").GetComponent<Renderer>().material.color = Color.black;
            }
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Tile t = field[x, z];
                SetTileNeigbours(t);
            }
        }
    }

    private void SetTileNeigbours(Tile tile)
    {
        int x = tile.posX;
        int z = tile.posZ;

       
        //Node node = new Node(tile, 1, field[x - 1, z - 1]);
      
        //bottom left
        if(x > 0 && z > 0)
        {
            Node node = new Node(tile, 1, field[x - 1, z - 1]);
        }
        //bottom middle
        if(z > 0)
        {
            Node node = new Node(tile, 1, field[x, z - 1]);
        }
        //bottom right
        if(x < sizeX - 1 && z > 0)
        {
            Node node = new Node(tile, 1, field[x + 1, z - 1]);
        }
        //middle left
        if(x > 0)
        {
            Node node = new Node(tile, 1, field[x - 1, z]);
        }
        //middle right
        if(x < sizeX - 1)
        {
            Node node = new Node(tile, 1, field[x + 1, z]);
        }
        //top left
        if(x > 0 && z < sizeZ - 1)
        {
            Node node = new Node(tile, 1, field[x - 1, z + 1]);
        }
        //top middle
        if(z < sizeZ - 1)
        {
            Node node = new Node(tile, 1, field[x, z + 1]);
        }
        //top right
        if(x < sizeX - 1 && z < sizeZ - 1)
        {
            Node node = new Node(tile, 1, field[x + 1, z + 1]);
        }
    }
}
