using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // for tilemap editing...

public class GridTester : MonoBehaviour
{
    public Grid testGrid;

    public Tilemap testTileMap; // get from grid???

    public Vector3 testTileMapSize = new Vector3(1f, 1f, 1f);

    public TileBase testTileBase; // a rule or basic tilest

    // Start is called before the first frame update
    void Start()
    {
        DebugGridDetails();

        FillTileMapSquare(16, testTileMap, testTileBase);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("GridTester mouesclick...");
            Vector3 mouseClickPosition = Input.mousePosition;
            Debug.Log("mousePosition = " + mouseClickPosition.ToString());

            int pixelsToTile; // 1920 
            Vector3 worldClickPosionToWorld = mouseClickPosition / 128;

            Vector3Int testCellVectorIntIndex = new Vector3Int(0,0,0);

            Vector3 testPositionVector = new Vector3(0.5f, 0.5f, 0.5f);

            testPositionVector = mouseClickPosition;

            testGrid.CellToLocal(testCellVectorIntIndex);
            testGrid.CellToLocalInterpolated(testCellVectorIntIndex);
            testGrid.CellToWorld(testCellVectorIntIndex);
            testGrid.GetBoundsLocal(testCellVectorIntIndex);

            testGrid.GetBoundsLocal(testCellVectorIntIndex);

            testGrid.GetCellCenterLocal(testCellVectorIntIndex);
            testGrid.GetCellCenterWorld(testCellVectorIntIndex);
            testGrid.GetLayoutCellCenter();

            testGrid.LocalToCell(testPositionVector);
            testGrid.LocalToCellInterpolated(testPositionVector);
            testGrid.LocalToWorld(testPositionVector);

            testGrid.WorldToCell(testPositionVector);
            testGrid.WorldToLocal(testPositionVector);

            //testGrid.cellGap.ToString();
            //testGrid.cellLayout.ToString();
            //testGrid.cellSize.ToString();
            //testGrid.cellSwizzle.ToString();

            //testGrid.
            Debug.Log("MousePos to Cell = " + testGrid.WorldToCell(mouseClickPosition).ToString());


        }
    }

    void DebugGridDetails()
    {
        Debug.Log(testGrid.name + ".GridTester.DebugGridDetails()");
        Debug.Log("cellGap = " + testGrid.cellGap.ToString());
        Debug.Log("cellLayout = " + testGrid.cellLayout.ToString());
        Debug.Log("cellSize = " + testGrid.cellSize.ToString());
        Debug.Log("cellSwizzle = " + testGrid.cellSwizzle.ToString());

        testTileMap = testGrid.GetComponentInChildren<Tilemap>(); // check if this is correct...

        // test tilemap details...
        //testTileMap.
    }

    public void FillTileMapSquare(int sideLength, Tilemap tilemap, TileBase tileBase)
    {
        Vector3Int tilePosition = new Vector3Int(0,0,0);

        for(int i = 0; i < sideLength*sideLength; i++)
        {
            tilePosition.x = Mathf.FloorToInt( i/sideLength);
            tilePosition.y = i%sideLength;

            tilemap.SetTile(tilePosition, tileBase);
        }
    }
}
