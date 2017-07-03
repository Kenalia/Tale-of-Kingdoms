using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable {

    private List<Activatable> connections;

    // ------ Begin Unity Methods ------

    private void Start()
    {
        connections = new List<Activatable>();
    }

    // ------ End Unity Methods ------

    // ------ Begin Non-Unity Methods ------

    public override void OnPrimaryInteract(GameObject user)
    {
        foreach(Activatable connection in connections) {
            connection.Activate();
        }
    }

    // ------ End Non-Unity Methods ------
}
