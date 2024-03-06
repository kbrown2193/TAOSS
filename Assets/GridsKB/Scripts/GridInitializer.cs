using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInitializer : MonoBehaviour
{
    public Grid grid;
    public Vector3Int gridBoundsMin = new Vector3Int(-4, -4, 0); // the loweest negative placement 
    public Vector3Int gridBoundsMax = new Vector3Int(3, 3, 0); // the highest tile placement


    public TileBase tile;

    public Tilemap tilemap;

    private void Awake()
    {
        tilemap = grid.GetComponentInChildren<Tilemap>();

        GenerateGrid();
    }


    void Start()
    {
        
    }

    [ContextMenu("Generate Grid")]
    void GenerateGrid()
    {

        // TODO: multiple ways for desired loading... 
        // simple fill
        // triple nested loop for now... 

        Vector3Int tilePosition = new Vector3Int(0, 0, 0);

        for (int ix = gridBoundsMin.x; ix <= gridBoundsMax.x; ix++)
        {
            tilePosition.x = ix;

            for(int iy = gridBoundsMin.y; iy <= gridBoundsMax.y; iy++)
            {
                for(int iz = gridBoundsMin.z; iz <= gridBoundsMax.z; iz++)
                {
                    tilePosition = new Vector3Int(ix, iy, iz);
                    tilemap.SetTile(tilePosition, tile);
                }

            }
        }
        // post loop cleanup?
    }
}
