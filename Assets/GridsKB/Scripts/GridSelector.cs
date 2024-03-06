using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class GridSelector : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Vector3Int gridBoundsMin = new Vector3Int(-4, -4, 0); // lower bounds of grid
    [SerializeField] private Vector3Int gridBoundsMax = new Vector3Int(3, 3, 0); // upper bounds of grid

    // just public right now for easy visibility
    public Vector3Int selectedTileIndex;
    //public Tile selectedTile;
    public TileBase selectedTile;

    public Vector2 gridSelectionMovement = new Vector2(0, 0);

    const float MOVEMENT_TRIGGER_THRESHOLD = 1f;


    // NOT CURRENTLY USED
    public float selectNextCooldown = 0.25f; // to prevent over scrolling, unless we want hypescroll (if Axis.X or axis.Y value is 1 for extending period of time  

    public float hyperscrollChargeTime = 3f; // after three seconds it will hyper scroll if continously pressed



    // pointer additional selection logic
    public Vector2 pointerPosition = new Vector2(0, 0);
    public Vector3 pointerWorldPosition = new Vector3(0, 0,0);
    public Vector3Int pointerHoveredTileIndex = new Vector3Int();
    public Camera camera; // required for? camera.ScreenToWorldPoint(context.ReadValue<Vector2>())     in OnPointerPosition from an action called PointerPosition which is a 2d vector


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Grid Seletion Functions
    public void SelectNextTileX()
    {
        if(selectedTileIndex.x < gridBoundsMax.x)
        {
            selectedTileIndex.x++;
        }
        else
        {
            // End Behavior
            // stop or wrap?
        }
    }
    public void SelectPreviousTileX()
    {
        if (selectedTileIndex.x > gridBoundsMin.x)
        {
            selectedTileIndex.x--;
        }
        else
        {
            // End Behavior
            // stop or wrap?
        }
    }
    public void SelectNextTileY()
    {
        if (selectedTileIndex.y < gridBoundsMax.y)
        {
            selectedTileIndex.y++;
        }
        else
        {
            // End Behavior
            // stop or wrap?
        }
    }
    public void SelectPreviousTileY()
    {
        if (selectedTileIndex.y > gridBoundsMin.y)
        {
            selectedTileIndex.y--;
        }
        else
        {
            // End Behavior
            // stop or wrap?
        }
    }
    public void SelectNextTileZ()
    {
        if (selectedTileIndex.z < gridBoundsMax.z)
        {
            selectedTileIndex.z++;
        }
        else
        {
            // End Behavior
            // stop or wrap?
        }
    }
    public void SelectPreviousTileZ()
    {
        if (selectedTileIndex.z > gridBoundsMin.z)
        {
            selectedTileIndex.z--;
        }
        else
        {
            // End Behavior
            // stop or wrap?
        }
    }



    #endregion

    #region Grid Events
    private void OnGridAction1()
    {
        // nouseclick 1? select if possible? or....issue comm
    }
    private void OnGridAction2()
    {

    }
    private void OnGridSelectionMovement(InputValue value)
    {
        gridSelectionMovement = value.Get<Vector2>();

        if(gridSelectionMovement.x >= MOVEMENT_TRIGGER_THRESHOLD)
        {
            SelectNextTileX();
        }

        if (gridSelectionMovement.x <= -MOVEMENT_TRIGGER_THRESHOLD)
        {
            SelectPreviousTileX();
        }

        if (gridSelectionMovement.y >= MOVEMENT_TRIGGER_THRESHOLD)
        {
            SelectNextTileY();
        }

        if (gridSelectionMovement.y <= -MOVEMENT_TRIGGER_THRESHOLD)
        {
            SelectPreviousTileY();
        }

    }
    private void OnGridElevationUp()
    {
        SelectNextTileZ();
    }
    private void OnGridElevationDown()
    {
        SelectPreviousTileZ();
    }

    private void OnGridPointerPosition(InputValue value)
    {
        pointerPosition = value.Get<Vector2>();
        pointerWorldPosition = camera.ScreenToWorldPoint(pointerPosition);
        pointerHoveredTileIndex = grid.WorldToCell(pointerWorldPosition);
    }
    #endregion


}
