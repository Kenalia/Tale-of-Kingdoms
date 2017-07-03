using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

	// ------ Begin Unity Methods ------
	
	// ------ End Unity Methods ------
	
	// ------ Begin Non-Unity Methods ------
	
    public virtual void Activate()
    {
        Debug.LogError("Activate Override not working!" + this.name);
    }

	// ------ End Non-Unity Methods ------
}
