using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Find a better system? structs and such...
/// for now, just making a functional version.
/// 
/// 
/// </summary>
public class CustomLevelLoadingTAOSS : MonoBehaviour
{
    [SerializeField] WorldLevelDatabase worldLevelDatabase;
    public void HandleLoadingLevel(string startingLevel, string destinationLevel)
    {
        Debug.Log("HandleLoadingLevel from " + startingLevel + " to " + destinationLevel);

        Player player = null; // find player? or use gm?

        if (startingLevel == destinationLevel)
        {
            Debug.LogWarning("Same level handle loading...");
            worldLevelDatabase.GetWorldLevelData(startingLevel);
        }
        else
        {
            // Can load level if is new level...
            WorldLevelData startingWorldLevelData = worldLevelDatabase.GetWorldLevelData(startingLevel);
            WorldLevelData destinationWorldLevelData = worldLevelDatabase.GetWorldLevelData(destinationLevel);

            //List<WorldLevel> worldLevelsInScene;
            WorldLevel[] worldLevelsInScene;

            if (destinationWorldLevelData != null)
            {
                if(destinationWorldLevelData.isUnlocked)
                {
                    Debug.Log("Unlocked");

                    if(startingWorldLevelData.worldLevelSceneName == destinationWorldLevelData.worldLevelSceneName)
                    {
                        Debug.Log("Same Scene");
                        // Handle activations / deactivations 
                        Debug.Log(startingWorldLevelData.worldLevelHierarchyName); // need to find this? and disable it
                        GameObject goToDisable = GameObject.Find(startingWorldLevelData.worldLevelHierarchyName); // should always be active if it was the scene we were just in...

                        if (GameObject.Find(startingWorldLevelData.worldLevelHierarchyName) != null)
                        {
                            Debug.Log("Starting Find Result Name = " + GameObject.Find(startingWorldLevelData.worldLevelHierarchyName).name);
                        }
                        else
                        {
                            Debug.LogWarning("Cannot find Stating level in hierarchy " + startingWorldLevelData.worldLevelHierarchyName);
                        }


                        Debug.Log("destination worldLevelHierarchyName = " + destinationWorldLevelData.worldLevelHierarchyName); // need to find this? and enable it

                        GameObject goToEnable = GameObject.Find(destinationWorldLevelData.worldLevelHierarchyName); // is the statement unecessary?

                        worldLevelsInScene = GameObject.FindObjectsOfType<WorldLevel>(true);
                        DebugPrintWorldLevelsArray(worldLevelsInScene);

                        goToEnable = GetWorldLevelGameObject(worldLevelsInScene, destinationWorldLevelData.worldLevelKey); // find the game object via finding by a script type


                        if (goToDisable != goToEnable)
                        {
                            Debug.Log("Different game object root");

                            if (goToDisable != null)
                            {
                                Debug.Log("Deactivating " + goToDisable.name);
                                // check if is above goToEnable
                                //  NEED TO FIX CASES for ...
                                string[] destinationRoots = (destinationWorldLevelData.worldLevelHierarchyName).Split('/');
                                if (destinationRoots.Length >= 4 )
                                {
                                    Debug.Log("nested scene case need to handle..."); // actually just enabling parents?
                                }

                                goToDisable.SetActive(false);
                            }
                            else
                            {
                                Debug.Log("GoToDeactivate is null");
                            }

                            if (goToEnable != null)
                            {
                                goToEnable.SetActive(true);
                                EnableParents(goToEnable); // will this fix goldenbox?
                            }                            
                            else
                            {
                                Debug.LogWarning("goToEnable is null");
                                // one last attempt

                            }

                            // IF NEW PLAYER IN SCENE, add flag in world level data?
                            GameManager.Instance.SetPlayer(player);

                            // for now.. go above

                            PositionPlayer(destinationWorldLevelData);

                            SetPlayerWorldLevelValues(destinationWorldLevelData);

                            ResizeCamera(destinationWorldLevelData);

                            //GameObject.Find(destinationWorldLevelData.worldLevelHiearchyName).SetActive(true);
                            //GameObject.Find(startingWorldLevelData.worldLevelHiearchyName).SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Same game object root");
                        }
                        // disable 
                    }
                    else
                    {
                        Debug.Log("Not same scene");
                        Debug.Log("StartingKey{" + startingWorldLevelData.worldLevelKey + "}, DestinationKey{" + destinationWorldLevelData.worldLevelKey +"}");
                        Debug.Log("StartingScene{" + startingWorldLevelData.worldLevelSceneName + "}, DestinationScene{" + destinationWorldLevelData.worldLevelSceneName +"}");
                    }
                }
                else
                {
                    Debug.Log("Locked");
                }
            }
            else
            {
                Debug.LogError("Invalid destinationWorldLevelData");
            }
        }
    }

    public GameObject GetWorldLevelGameObject(WorldLevel[] worldLevels, string targetWorldLevelKey)
    {
        if (worldLevels == null)
        {
            Debug.Log("NULL World Levels");
            return null;
        }
        else
        {
            foreach (WorldLevel wl in worldLevels)
            {
                Debug.Log(wl.WorldLevelKey);
                if(wl.WorldLevelKey == targetWorldLevelKey)
                {
                    return wl.gameObject;
                }

                //Debug.Log(wl,get);
            }

            return null;
        }
    }
    public void DebugPrintWorldLevelsArray(WorldLevel[] worldLevels)
    {
        Debug.Log("WorldLevelsFound " + worldLevels.Length);
        foreach(WorldLevel wl in worldLevels)
        {
            Debug.Log(wl.WorldLevelKey);
            //Debug.Log(wl,get);
        }
    }

    public void ReparentPlayer(WorldLevelData worldLevelData)
    {
        Debug.Log("Reparenting player... NOT SETUP?");
        // 
        //GameManager.Instance.ReparentPlayer(worldLevelData)
    }
    public void PositionPlayer(WorldLevelData worldLevelData )
    {
        Debug.Log("Positioning Player");
        GameManager.Instance.RepositionPlayer(worldLevelData.playerSpawnPosition);
    }
    public void ResizeCamera(WorldLevelData worldLevelData)
    {
        Debug.Log("Resizing Camera");
        float yMultiplier = 0.1f; // should be just a right shift? so 6 goes to .6 ... 80 would go to .80?  
        Camera.main.orthographicSize = (float)worldLevelData.cameraSize.x + worldLevelData.cameraSize.y * yMultiplier;
        Debug.LogWarning("Will not work for cameraSize.y >= 10 ");
    }
    public void SetPlayerWorldLevelValues(WorldLevelData worldLevelData)
    {
        Debug.Log("Setting Player World Level Values...");
        GameManager.Instance.SetPlayerWorldLevelSizeScaler(worldLevelData.worldLevelSizeMultiplier);
        GameManager.Instance.SetPlayerWorldLevelSpeedMultiplier(worldLevelData.worldLevelMovementSpeedMultiplier);
    }

    public Player FindActivePlayer()
    {
        return GameObject.FindAnyObjectByType<Player>();
    }

    public void EnableParents(GameObject go)
    {
        Debug.Log("Enabling parents for " + go.name);

        if(go.transform.parent != null)
        {
            Debug.Log("Enabling parent" + go.transform.parent.gameObject.name);

            go.transform.parent.gameObject.SetActive(true);

            if(go.transform.parent.transform.parent != null)
            {
                // recuersive call if still has parent.
                Debug.Log("Recursive Call started at " + go.name + " for " + go.transform.parent.gameObject.name);
                EnableParents(go.transform.parent.gameObject);
            }
        }
    }
}
