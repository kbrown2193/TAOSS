using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWobbler : MonoBehaviour
{
    public bool isActive;
    public bool isCoroutineActive;

    public float wobbleSpeed = 1f;

    public Transform targetTransform;

    public Vector3 rotationAmount = new Vector3(10, 0, 0);
    public Vector3 rotationVector;

    private void Update()
    {
        if (isActive)
        {
            if (isCoroutineActive)
            {
                // do nothing, courtine should be running
            }
            else

            {
                StartCoroutine(GridWobble());
            }
        }
    }

    public IEnumerator GridWobble()
    {

        while (isActive)
        {
            rotationVector = new Vector3(rotationAmount.x * Mathf.Sin(Time.time*wobbleSpeed), rotationAmount.y * Mathf.Sin(Time.time * wobbleSpeed), rotationAmount.z * Mathf.Sin(Time.time * wobbleSpeed));

            targetTransform.rotation = Quaternion.Euler(rotationVector);
            yield return null;
        }
        yield return null;
    }
}
