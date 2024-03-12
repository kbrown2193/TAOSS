using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatewaySeal : MonoBehaviour
{
    private bool[] locksIsLocked = new bool[4] {true,true,true,true}; // defaul set all locks locked... using these for the logic checks...
    private bool isUnlocked = false;
    private LockData[] lockDatas = new LockData[4]; //  for saving...

    [SerializeField] private SpriteRenderer[] lockSpriteRenderers = new SpriteRenderer[4]; // the locke sprites in game to set...
    [SerializeField] private Sprite[] lockSpritesLocked = new Sprite[4]; // the objects in game to set...
    [SerializeField] private Sprite[] lockSpritesUnlocked = new Sprite[4]; // the objects in game to set...

    [SerializeField] private SpriteRenderer doorSpriteRenderer; // the in game sprite for the door
    [SerializeField] private Sprite doorSpriteLocked; // set door to this if locked
    [SerializeField] private Sprite doorSpriteUnlocked; // set door to this if unlocked

    /*
    private void Update()
    {
        // testing Locks
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Locking All");
            for(int i = 0; i < locksIsLocked.Length; i++)
            {
                SetLockIsLocked(i, true);
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Unlocking All");
            for (int i = 0; i < locksIsLocked.Length; i++)
            {
                SetLockIsLocked(i, false);
            }
        }
    }
    */

    #region Locks
    public bool [] LocksIsLocked
        { get { return locksIsLocked; } }

    public bool [] GetLocksIsLocked() {
        return locksIsLocked; }

    public void SetLockIsLocked(int index, bool value)
    {
        Debug.Log("Setting Lock is Locked" + index + "] to " + value);
        locksIsLocked[index] = value;
        SetLockSpriteBasedOnState(index);
        CheckIfGatewayIsUnlocked();
        SetDoorSpriteBasedOnState();
        DebugPrint();
    }

    public bool GetLockIsLocked(int index)
    {
        Debug.Log("Getting Lock is Locked" + index + "] = " + locksIsLocked[index]);
        return locksIsLocked[index];
    }

    public bool CheckIfGatewayIsUnlocked()
    {
        isUnlocked = !(locksIsLocked[0] && locksIsLocked[1] && locksIsLocked[2] && locksIsLocked[3]); // is unlocked is only true if all locks are unlocked
        //Debug.Log("Gateway seal is unlocked = " + isUnlocked);
        DebugPrint();
        return isUnlocked;
    }

    public void DebugPrint()
    {
        Debug.Log("Gateway seal is unlocked = " + isUnlocked);
        for(int i = 0 ; i < locksIsLocked.Length; i++)
        {
            Debug.Log("Lock[" + i + "] = " + locksIsLocked[i]);
        }
    }
    #endregion

    #region Saving and Loading Data
    public void SaveLockDatas()
    {
        // set lock data from islocked
        for (int i = 0; i < locksIsLocked.Length; i++)
        {
            // setting data
            lockDatas[i].lockKey = "Gateway_Lock_"+i.ToString(); // may be unecessary if initialized correctly?
            lockDatas[i].lockIndex = i;
            lockDatas[i].isLocked = LocksIsLocked[i];
            //Debug.Log("Lock[" + i + "] = " + locksIsLocked[i]);
        }

        Debug.Log("TODO: WHERE TO SAVE THIS DATA... TODO");
        // something like...
        //GameManager.SaveGatewayLocksDatas(LockData lockDatas)
    }
    // TODO: LOAD DATA
    public void LoadLockDatas()
    {
        Debug.Log("TODO: Load Lock Data");
        // something like 
        //LockData[] returnsALockDataArray = GameManager.LoadGatewayLocksDatas()
    }
    #endregion

    #region Sprites
    public void SetDoorSpriteBasedOnState()
    {
        if(isUnlocked)
        {
            doorSpriteRenderer.sprite = doorSpriteUnlocked;
        }
        else
        {
            doorSpriteRenderer.sprite = doorSpriteLocked;
        }
    }

    public void SetLockSpriteBasedOnState(int lockIndex)
    {
        if (locksIsLocked[lockIndex])
        {
            lockSpriteRenderers[lockIndex].sprite = lockSpritesLocked[lockIndex];
        }
        else
        {
            lockSpriteRenderers[lockIndex].sprite = lockSpritesUnlocked[lockIndex];
        }
    }

    public void SetLocksSpritesBasedOnState()
    {
        for(int i = 0; i < lockSpriteRenderers.Length; i++)
        {
            SetLockSpriteBasedOnState(i);
        }
    }
    #endregion
}

[System.Serializable]
public class LockData
{
    public string lockKey; // not the key to open the door, but the key as in a dictionary...
    public bool isLocked;
    public int lockIndex; // index in an array
    //public string Description;
}
