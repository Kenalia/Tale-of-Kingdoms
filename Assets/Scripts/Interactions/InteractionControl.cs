using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControl : MonoBehaviour {

    //Control Variables
    public bool mounted = false;
    public bool hasControl = true;
    public bool hasGrapplingCapability = true;

    //Placing Variables
    private bool isPlacingObject = false;
    private GameObject objectBeingPlaced = null;
    [SerializeField] private float maxPlaceRange = 3.0f;
    [SerializeField] private string placeableOnTag;

    //Inventory Variables
    public Inventory_Player inventory = null;

    //Collider/Tracking Variables
    [SerializeField] private SphereCollider interactionCollider;
    [SerializeField] float interactionColliderRadius;
    [SerializeField, ReadOnly] private List<Interactable> itemsInRange;

    //Connection/Linking Variables
    Interactable connected;

    private void Start()
    {
        if(interactionCollider == null)
        {
            interactionCollider = gameObject.AddComponent<SphereCollider>();
            interactionCollider.isTrigger = true;
            interactionCollider.radius = interactionColliderRadius;
        }

        if (hasGrapplingCapability)
        {
            GameObject grappling = new GameObject("Grappling", typeof(GrapplingControl));
            grappling.transform.parent = this.transform;
            grappling.GetComponent<GrapplingControl>().playerInv = inventory;
        }
    }

    private void Update()
    {
        if (isPlacingObject)
        {
            PlacingObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            if(other.GetComponent<HookPoint>() == false)
            {
                itemsInRange.Add(other.GetComponent<Interactable>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            if (itemsInRange.Contains(other.GetComponent<Interactable>()))
            {
                itemsInRange.Remove(other.GetComponent<Interactable>());
            }
        }
    }

    // ------ Begin Non-Unity Methods ------

    public void HandlePrimaryInteract(Interactable interacted)
    {
        interacted.OnPrimaryInteract(transform.parent.gameObject);
    }

    public void HandleSecondaryInteract(Interactable interacted)
    {
        interacted.OnSecondaryInteract(transform.parent.gameObject);
    }

    /// <summary>
    /// Checks the player's current looking direction to see if they are directly looking at a non-HookPoint interactable
    /// </summary>
    /// <returns>If the player can grapple or not</returns>
    public bool CheckIfCanGrapple()
    {
        RaycastHit hitInfo;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, interactionColliderRadius);


        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.GetComponent<Interactable>() && hitInfo.collider.gameObject.GetComponent<HookPoint>() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    // --- Handles Object Placing --- //

    public void SetObjectToBePlaced(GameObject newObjectToBePlaced)
    {
        //Make sure this is an Activatable.
        if (GetComponent<Activatable>())
        {
            objectBeingPlaced = newObjectToBePlaced;
        }
        else if (GetComponentInChildren<Activatable>())
        {
            objectBeingPlaced = newObjectToBePlaced;
        }
        else if (GetComponentInParent<Activatable>())
        {
            objectBeingPlaced = newObjectToBePlaced;
        }

        isPlacingObject = true;
    }

    /// <summary>
    /// Handles the placing of Activatable objects. This is the main Placing loop.
    /// </summary>
    private void PlacingObject()
    {
        Ray placePoint = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        Physics.Raycast(placePoint, out hitInfo, maxPlaceRange);

        if (hitInfo.collider != null && hitInfo.collider.tag == placeableOnTag)
        {
            
        }
    }

    // --- End Object Placing --- //

    // ------ End Non-Unity Methods ------
}
