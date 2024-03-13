using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewaySealLockTrigger : MonoBehaviour
{
    [SerializeField]
    private GatewaySeal gatewaySeal;
    [SerializeField]
    private int lockNumber = 0;

    [SerializeField]
    bool willUnlock = true; // if true, entering this will unlock

    private bool isEnabled = false;

    private Collider2D triggerCollider2D;

    private void Awake()
    {
        if (gatewaySeal == null)
        {
            Debug.LogError("Gateway Seal is Null, attempting to find");
            gatewaySeal = GameObject.FindAnyObjectByType<GatewaySeal>();
        }
        if (gatewaySeal == null)
        {
            Debug.LogError("Gateway Seal is still Null");
            gatewaySeal = GameObject.FindAnyObjectByType<GatewaySeal>();
        }
    }

    public void EnableTrigger()
    {
        isEnabled = true;
    }
    public void DisableTrigger()
    {
        isEnabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isEnabled)
        {
            Debug.Log("Trigger Disabled");
            return;
        }
        else
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            if (col.tag == "Player")
            {
                Debug.Log(this.name + ".Player Tag Detected");
                if (willUnlock)
                {
                    UnlockGatewayLock();
                }
                else
                {
                    LockGatewayLock();
                }
            }
        }
    }

    public void UnlockGatewayLock()
    {
        Debug.Log("UnLocking...");
        if (gatewaySeal != null)
        {
            gatewaySeal.SetLockIsLocked(lockNumber, false);
        }
        else
        {
            Debug.LogError("Gateway Seal is Null!");
        }
    }

    public void LockGatewayLock()
    {
        Debug.Log("Locking...");
        if (gatewaySeal != null)
        {
            gatewaySeal.SetLockIsLocked(lockNumber, true);
        }
        else
        {
            Debug.LogError("Gateway Seal is Null!");
        }
    }

    public void SetLockNumber(int value)
    {
        lockNumber = value;
    }

    public int GetLockNumber()
    {
        return lockNumber;
    }

    public void SetWillUnlock(bool value)
    {
        willUnlock = value;
    }

    public bool GetWillUnlock()
    {
        return willUnlock;
    }
}
