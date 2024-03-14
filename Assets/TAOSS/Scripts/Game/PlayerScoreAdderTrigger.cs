using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreAdderTrigger : MonoBehaviour
{
    public int amount = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.tag == "Player")
        {
            Debug.Log(this.name + ".Player Tag Detected");
            GameManager.Instance.AddPlayerScore(amount);
            // Cleanup... For now destroy
            Destroy(this.gameObject);
        }
    }
}
