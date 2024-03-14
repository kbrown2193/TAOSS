using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTrap : MonoBehaviour
{
    [SerializeField]
    private int amount = 1;

    [SerializeField]
    private bool isOneTime = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.tag == "Player")
        {
            Debug.Log(this.name + ".Player Tag Detected");
            GameManager.Instance.HealPlayer(amount);

            if(isOneTime)
            {
                Debug.Log("One time use healing... Destroying...");
                Destroy(this.gameObject);
            }
        }
    }
}
