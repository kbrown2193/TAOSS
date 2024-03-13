using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBoxLevelCompleter : MonoBehaviour
{
    [SerializeField]
    GoldenboxGameFlowManager gameFlowManager;
    public int levelIndex = 0; // the level index

    //private int levelIndex; // for now 0 is bird, 1 is beast... syn
    private string levelKey;

    public bool isEnabled = true;

    public void CompleteLevel()
    {
        Debug.Log("Level Complete");
        if(levelIndex  < 4)
        {
            Debug.Log("Unlocking Gateway Seal" + levelIndex);

            gameFlowManager.EnableGatewaySealLockTrigger(levelIndex, true);
        }
        else if(levelIndex < 5)
        {
            Debug.Log("Gateway Complete");
        }
        else if(levelIndex == 5)
        {
            Debug.Log("Artifacts Complete");
        }


        GameManager.Instance.CompleteLevel(levelIndex); // GAME MANAGER HANDLING outside portal activation
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isEnabled)
        {
            Debug.Log("TAOSSLevelCompleter Disabled");
            return;
        }
        else
        {
            Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            if (col.tag == "Player")
            {
                CompleteLevel();
            }
        }
    }
}
