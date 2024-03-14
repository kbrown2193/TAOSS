using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingTrap : MonoBehaviour
{
    [SerializeField] private Vector3 teleportPlayerToPosition = new Vector3(0f, 0f, 0f);

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.tag == "Player")
        {
            Debug.Log(this.name + ".Player Tag Detected");
            GameManager.Instance.RepositionPlayer(teleportPlayerToPosition);
        }
    }
}
