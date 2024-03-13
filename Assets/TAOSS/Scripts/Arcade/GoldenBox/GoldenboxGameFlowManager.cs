using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenboxGameFlowManager : MonoBehaviour
{
    [SerializeField] GatewaySeal gatewaySeal;
    [SerializeField] GatewaySealLockTrigger[] gatewaySealLockTriggers;

    void Awake()
    {
        RefreshGatewayLockTriggers();
    }

    #region Lock Trigger Setting and Enabling
    public void RefreshGatewayLockTriggers()
    {
        Debug.Log("Load the triggers based on gamesdata?");
        LockData[] lockDatas = GameManager.Instance.gameData.gatewaylockDatas;
        if(lockDatas !=null)
        {
            for(int i = 0; i < lockDatas.Length; i++)
            {
                EnableGatewaySealLockTrigger(i, !lockDatas[i].isLocked); // set enable to true IF lockData is NOT isLocked
            }
        }
        else
        {
            Debug.LogError("GameData is null!");
        }
    }
    public void DisableAllGatewayLockTriggers()
    {
        for(int i = 0; i < gatewaySealLockTriggers.Length; i++)
        {
            DisableGatewaySealLockTrigger(i);
        }
    }
    public void EnableAllGatewayLockTriggers()
    {
        for (int i = 0; i < gatewaySealLockTriggers.Length; i++)
        {
            EnableGatewaySealLockTrigger(i);
        }
    }

    public void DisableGatewaySealLockTrigger(int index)
    {
        Debug.Log("Disabling GatewaySealLockTrigger " + index);
        gatewaySealLockTriggers[index].DisableTrigger();
    }
    public void EnableGatewaySealLockTrigger(int index)
    {

        Debug.Log("Enabling GatewaySealLockTrigger " + index);
        gatewaySealLockTriggers[index].EnableTrigger();
    }
    public void EnableGatewaySealLockTrigger(int index, bool isEnabled)
    {
        Debug.Log("Enabling GatewaySealLockTrigger " + index + " to " + isEnabled);
        if (isEnabled)
        {
            EnableGatewaySealLockTrigger(index);
        }
        else
        {
            DisableGatewaySealLockTrigger(index);
        }
    }

    /// <summary>
    /// Controls whether this trigger will unlock a seal (or "Lock" it)
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value">True if the trigger will unlock the seal</param>
    public void SetGateSealLockTriggerWillUnlock(int index, bool value)
    {
        gatewaySealLockTriggers[index].SetWillUnlock(value);
    }
    #endregion
}
