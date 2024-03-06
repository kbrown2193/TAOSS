using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGridSizeExpansion : MonoBehaviour
{
    public Grid grid;
    public Vector3 startingSize = new Vector3(1f, 0.5f, 0f);
    public Vector3 targetSize = new Vector3(1f, 1, 0f);

    public float changeDuration = 10f;
    public float initialWait = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeGridSize());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChangeGridSize()
    {
        if(initialWait > 0f)
        {
            yield return new WaitForSeconds(initialWait);
        }

        Vector3 changeToVector = startingSize;

        grid.cellSize = changeToVector;

        float timer = 0f;
        //Debug.Log("Time has taken for movement corutine duration for " + this.name + " = " + timer);
        float progress = timer / changeDuration; // 0 duration is error

        while (timer < changeDuration)
        {
            timer += Time.deltaTime;
            progress = timer / changeDuration;

            changeToVector = ((1-progress)*startingSize +   (progress * targetSize));


            grid.cellSize = changeToVector;

            yield return null;
        }


        grid.cellSize = targetSize;

        yield return null;

    }
}
