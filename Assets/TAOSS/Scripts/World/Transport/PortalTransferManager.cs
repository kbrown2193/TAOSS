using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTransferManager : MonoBehaviour
{
    [SerializeField]
    private PortalsDatabase portalsDatabase; // Assign this asset to change portals

    #region Singleton
    private static PortalTransferManager _instance;

    public static PortalTransferManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PortalTransferManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("PortalTransferManager");
                    _instance = singletonObject.AddComponent<PortalTransferManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public void AttemptPortalTransfer(string portalKey)
    {
        // need to check if portal is unlocked
        // todo make more robust, for now just making it work...
        PortalData portalData = portalsDatabase.GetPortalData(portalKey);
        //portalDataLookup.TryGetValue(portalKey, out PortalData portalData);
        if(portalData != null)
        {
            Debug.Log(portalData.portalKey + " is unlocked = " + portalData.isUnlocked.ToString());
            Debug.Log(portalData.portalKey + " Portal / Destination Key " + portalData.destinationKey);
            Debug.Log(portalData.portalLevel + " Portal / Destination Level " + portalData.destinationLevel);
            Debug.Log(portalData.portalPosition + " Portal / Destination Piosition " + portalData.destinationPosition);
            Debug.Log(portalData.portalDirection + " Portal / Destination Direction " + portalData.destinationDirection);

            if(portalData.isUnlocked)
            {
                Debug.Log("Portal iss unlocked, can start transfer...");

                if(portalData.portalLevel == portalData.destinationLevel)
                {
                    Debug.Log("Same Level Portal...");
                    // handle destination LOCAL events?
                    // handle player teleport, caera resizing / repositioning,                     
                }
                else
                {
                    Debug.Log("Different Level Portal");
                    // handle loading new level
                    // handle unloading old level

                    // hanle player positioning, camera details
                }
            }
            else
            {
                Debug.Log("Portal is locked... try some other time");
            }
        }
        else
        {
            Debug.LogWarning("portal data is Null!!!");
        }
    } 
}
