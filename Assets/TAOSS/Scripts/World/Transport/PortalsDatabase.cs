using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PortalsDatabase", menuName = "World/Transport/Portals Database")]
public class PortalsDatabase : ScriptableObject
{
    [SerializeField] private List<PortalData> portalDataList = new List<PortalData>();

    private Dictionary<string, PortalData> portalLookupTable = new Dictionary<string, PortalData>();

    private void OnEnable()
    {
        // Populate the lookup table when the scriptable object is loaded
        InitializeLookupTable();
    }

    private void InitializeLookupTable()
    {
        portalLookupTable.Clear();
        foreach (PortalData portalData in portalDataList)
        {
            portalLookupTable[portalData.portalKey] = portalData;
        }
    }

    // Function to retrieve a PortalData based on portalKey
    public PortalData GetPortalData(string portalKey)
    {
        if (portalLookupTable.ContainsKey(portalKey))
        {
            return portalLookupTable[portalKey];
        }
        else
        {
            Debug.LogWarning("portalKey with key " + portalKey + " not found in the database.");
            return null;
        }
    }
}

[System.Serializable]
public class PortalData
{
    public string portalKey;
    public bool isUnlocked; // if is unlocked... can use ... go to destination 
    public string destinationKey; // may be unecessary if always 1 to 1 or storing info in here anyways...
    public string portalLevel; // the level in which the portal is
    public string destinationLevel; // the level in which the destinatinon is
    public Vector3 portalPosition; // the position the portal is in 
    public Vector3 portalDirection; // the direction the portal is facing
    public Vector3 destinationPosition; // position to spawn player at
    public Vector3 destinationDirection; // rotation to spawn player at
    public Vector2Int destinationCameraSize; // this is split into 2 ints, the WHOLE portion and the FRACTIONAL portion, to avoid storing floats... need to convert to float?
}
