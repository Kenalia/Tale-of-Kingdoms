using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPoint : Interactable {

    // ------ Begin Unity Methods ------

    // ------ End Unity Methods ------

    // ------ Begin Non-Unity Methods ------

    public override void OnSecondaryInteract(GameObject user)
    {
        Debug.Log("HookPoint called");
        user.transform.position = transform.position;
    }

    // ------ End Non-Unity Methods ------

}
