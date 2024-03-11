using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "WorldLevelDatabase", menuName = "World/Level/Level Database")]

public class WorldLevelDatabase : ScriptableObject
{
    [SerializeField] private List<WorldLevelData> worldLevelDataList = new List<WorldLevelData>();

    private Dictionary<string, WorldLevelData> worldLevelLookupTable = new Dictionary<string, WorldLevelData>();

    private void OnEnable()
    {
        // Populate the lookup table when the scriptable object is loaded
        InitializeLookupTable();
    }

    private void InitializeLookupTable()
    {
        worldLevelLookupTable.Clear();
        foreach (WorldLevelData portalData in worldLevelDataList)
        {
            worldLevelLookupTable[portalData.worldLevelKey] = portalData;
        }
    }

    // Function to retrieve a PortalData based on portalKey
    public WorldLevelData GetWorldLevelData(string worldLevelKey)
    {
        if (worldLevelLookupTable.ContainsKey(worldLevelKey))
        {
            Debug.Log("WL.DB.Looking up worldLevelKey = " + worldLevelKey);
            return worldLevelLookupTable[worldLevelKey];
        }
        else
        {
            Debug.LogWarning("worldLevelKey with key " + worldLevelKey + " not found in the database.");
            return null;
        }
    }
}
[System.Serializable]
public class WorldLevelData
{
    public string worldLevelKey; // should be = portalData.portalLevel
    public bool isUnlocked; // if is unlocked... can use ... can go here

    // scene info / grid info
    public string worldLevelSceneName; // TAOSS_00
    public string worldLevelHierarchyName; // GameWorld_C0_L00/Arcade/SilverRoom/GoldenBox
    // wherwe GoldenBox is the game object name for this level

    // grids (if not serializable like this convert to? or be GridData[], and WorldLevelTileMapData[]
    // either that or file read...load game data for this world level key?... but lets just store data here for now...
    public Grid[] worldLevlGrids;
    public Tilemap[] worldLevelTilemaps;

    // Positioning and Loading info
    public Vector3 playerSpawnPosition; // the position the player is spawned at (or at least moved to...)
    public Vector3 playerSpawnDirection; // the direction the player is 
    public Vector3 cameraSpawnPosition; // the position the camera is at
    public Vector3 cameraSpawnDirection; // the direction the camera is facing
    public Vector2Int cameraSize; // this is split into 2 ints, the WHOLE portion and the FRACTIONAL portion, to avoid storing floats... need to convert to float?

    public Vector3Int playerCellSpawnPosition; // the corresponding cell in the grid to spawn in (for which grid? assume 1? for now...)

    public float worldLevelSizeMultiplier;
    public float worldLevelMovementSpeedMultiplier;
    //public Vector3Int playerCellSpawnPosition; // entities spawn positions... ?

    // entities

    // ground loot

    // objectives

}
