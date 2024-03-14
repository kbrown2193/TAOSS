using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTrap : MonoBehaviour
{
    [SerializeField]
    private int amount;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.tag == "Player")
        {
            Debug.Log(this.name + ".Player Tag Detected");
            GameManager.Instance.DamagePlayer(amount);
        }
    }
}
