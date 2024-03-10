using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string portalKey; // the key too this portal maybe make int... for now is string for dictionary lookup
    public string GetPortalKey()
    {
        return portalKey;
    }    
    public void SetPotalKey(string  newKey)
    {
        portalKey = newKey;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.tag == "Player")
        {
            Debug.Log(this.name + ".Player Tag Detected");
            Debug.Log("Initiating Portal Transfer.");
            PortalTransferManager.Instance.AttemptPortalTransfer(portalKey);            
        }
    }
}
