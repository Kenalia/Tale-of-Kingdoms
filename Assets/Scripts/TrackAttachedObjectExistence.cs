using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAttachedObjectExistence : MonoBehaviour {

    public GameObject attachedObject = null;

    // ------ Begin Unity Methods ------

    private void Update()
    {
        if(attachedObject == null)
        {
            DestroyFullObject();
        }
    }

    // ------ End Unity Methods ------

    // ------ Begin Non-Unity Methods ------

    private void DestroyFullObject()
    {
        bool highestParentFound = false;
        GameObject highestParent = this.gameObject;

        while (highestParentFound == false)
        {
            if (highestParent.transform.parent == null)
            {
                highestParentFound = true;
            }
            else
            {
                highestParent = highestParent.transform.parent.gameObject;
            }
        }
    }

    // ------ End Non-Unity Methods ------
}
