using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveScaler : MonoBehaviour
{
    public Transform scaledTransform;

    public float worldLevelSizeScaler = 1; //  default 1, set based on level, player might be LARGE *48 like silver room...

    // for current iteration, the focus horizon is always at y = 0 ... then
    public float farPositionValue = 2f; // MUST BE POSITIVE
    public float nearPositionValue = -10f; // MUST BE NEGATIVE

    public float farSmallSizeFactor = 0.25f; // the size we want to shrink to ( MUST BE SMALLER THAN 1)
    public float nearLargeSizeFactor = 3f; // the size we wwant to enlarge to ( MUST BE LARGER THAN 1)

    public float farUpperLimit = 2f; // incase does not match position value, clamp here // clamp limit...
    public float nearLowerLimit = -10f;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current Position = " + transform.position.ToString());
        //Debug.Log("Current scale = " + transform.localScale.ToString());
        ScaleTransformByWorldPosition();
    }

    public void ScaleTransformByWorldPosition()
    {
        Vector3 tmpPos = scaledTransform.position;
        Vector3 tmpScale = scaledTransform.localScale; // if no calcs, keep initial scale?

        // Calclulate factors
        float farScaleFact = tmpPos.y / farPositionValue; 
        float farScaler = 1 - farScaleFact * (1-farSmallSizeFactor); // This could use some cleanup, but keeping for readability for now

        float nearScaleFact =  ( tmpPos.y / nearPositionValue);
        float nearScaler = 1 + (nearScaleFact * (nearLargeSizeFactor - 1)); // This could use some cleanup, but keeping for readability for now

        //Debug.Log("farScaler = " + farScaler.ToString());
        //Debug.Log("nearScaler = " + nearScaler.ToString());

        if (tmpPos.y > 0)
        {
            // is farther away
            //Debug.Log("Farther away... should be smaller than 1");

            if(tmpPos.y >= farUpperLimit)
            {
                // clamp here change sscaler
                farScaler = 1 - 1 * (1 - farSmallSizeFactor);
            }
            else
            {
                farScaler = 1 - farScaleFact * (1 - farSmallSizeFactor); // is calculated twice currently... also above, leave for now....
            }

            tmpScale = farScaler * Vector3.one;
        }
        else
        {
            // is at zero ?

            // is closer
            //Debug.Log("Closer... should be greater than 1");
            if(tmpPos.y <= nearLowerLimit)
            {
                nearScaler = 1 + (1 * (nearLargeSizeFactor - 1));
            }
            else
            {
                nearScaler = 1 + (nearScaleFact * (nearLargeSizeFactor - 1));
            }

            tmpScale =  nearScaler * Vector3.one;
        }

        tmpScale = tmpScale * worldLevelSizeScaler; // 

        // scale the transform
        scaledTransform.localScale = tmpScale;
    }

    public void SetWorldLevelSizeScaler(float value)
    {
        worldLevelSizeScaler = value;
    }

}
