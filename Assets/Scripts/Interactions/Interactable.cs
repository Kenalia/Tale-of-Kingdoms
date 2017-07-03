using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	virtual public void OnPrimaryInteract(GameObject user)
    {
        Debug.Log("Override not working!");
    }

    virtual public void OnSecondaryInteract(GameObject user)
    {
        Debug.Log("Override not working!");
    }

    // ------ End Non-Unity Methods ------
}
