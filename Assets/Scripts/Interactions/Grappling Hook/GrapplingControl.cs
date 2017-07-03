using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingControl : MonoBehaviour {

    public Inventory_Player playerInv = null;

    [SerializeField] private float hookFinderColliderRadius = 6.0f;
    [SerializeField] private float minRequiredDistance = 2.0f;

    [SerializeField, ReadOnly] private SphereCollider hookFinderCollider;
    [SerializeField, ReadOnly] private List<HookPoint> hookPointsInRange;
    [SerializeField, ReadOnly] private bool canGrapple = false;
    [SerializeField, ReadOnly] private HookPoint targetedHookPoint = null;

    // ------ Begin Unity Methods ------

    private void Start()
    {
        hookFinderColliderRadius = playerInv.grappleHookEquipped.grappleRange;

        if (hookFinderCollider == null)
        {
            hookFinderCollider = gameObject.AddComponent<SphereCollider>();
            hookFinderCollider.isTrigger = true;
            hookFinderCollider.radius = hookFinderColliderRadius;
        }

        hookPointsInRange = new List<HookPoint>();
    }

    private void Update()
    {

        //Don't bother getting the targeted hook if there are none in range
        if(hookPointsInRange.Count > 0)
        {
            if(hookPointsInRange.Count == 1)
            {
                targetedHookPoint = VerifyHookPointDistance(hookPointsInRange[0]) ? hookPointsInRange[0] : null;
            }
            else
            {
                targetedHookPoint = FindTargetedHookPoint();
            }
        }

        //If we do happen to have one targeted, check with InteractionControl to make sure that we aren't currently targeting an item
        if(targetedHookPoint != null)
        {
            canGrapple = GetComponentInParent<InteractionControl>().CheckIfCanGrapple();
        }

        if (Input.GetKeyDown(InputManager.IM.KEY_interact_secondary) && canGrapple)
        {
            GetComponentInParent<InteractionControl>().HandleSecondaryInteract(targetedHookPoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hookPointsInRange.Contains(other.GetComponent<HookPoint>())){

        }
        else if (other.GetComponent<HookPoint>())
        {
            hookPointsInRange.Add(other.GetComponent<HookPoint>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HookPoint>())
        {
            if (hookPointsInRange.Contains(other.GetComponent<HookPoint>()))
            {
                hookPointsInRange.Remove(other.GetComponent<HookPoint>());
            }
        }
    }

    // ------ End Unity Methods ------

    // ------ Begin Non-Unity Methods ------

    /// <summary>
    /// Searches through all hook points in range for the closest 
    /// </summary>
    private HookPoint FindTargetedHookPoint()
    {
        HookPoint determinedTarget = null;

        HookPoint closestToPlayer = null;
        HookPoint closestToLookPoint = null;
        float distanceFromPlayer = 0.0f;
        float distanceFromLookPoint = 0.0f;

        //Get the closest hook point to the player's current position
        distanceFromPlayer = GetClosestToPoint(transform.position, ref closestToPlayer);

        //Cast from the center cursor and see if it hits anything.
        RaycastHit hitInfo;
        Ray lookPoint = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Physics.Raycast(lookPoint, out hitInfo, hookFinderColliderRadius);

        //If the raycast hit anything, find the closest HookPoint from the point of impact
        if(hitInfo.collider != null)
        {
            distanceFromLookPoint = GetClosestToPoint(hitInfo.transform.position, ref closestToLookPoint);
            Debug.Log(closestToLookPoint.name);
        }
        //If it did not hit anything, find the closest HookPoint from the max point of the ray
        else
        {
            Vector3 maxLookPoint = lookPoint.GetPoint(hookFinderColliderRadius);

            distanceFromLookPoint = GetClosestToPoint(maxLookPoint, ref closestToLookPoint);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hookFinderColliderRadius, Color.red);
        }

        if(closestToPlayer != null && closestToLookPoint != null)
        {
            Debug.Log("neither null");
            determinedTarget = (distanceFromLookPoint < distanceFromPlayer) ? closestToLookPoint : closestToPlayer;
            Debug.Log(determinedTarget.name);
        }
        else if(closestToPlayer == null && closestToLookPoint != null)
        {
            determinedTarget = closestToLookPoint;
        }
        else if(closestToPlayer != null && closestToLookPoint == null)
        {
            determinedTarget = closestToPlayer;
        }
        else
        {
            determinedTarget = null; // Just in case :)
        }

        return determinedTarget;
    }

    private float GetClosestToPoint(Vector3 centerPosition, ref HookPoint toBeAssigned)
    {
        float closestDistance = 0.0f;

        for (int i = 0; i < hookPointsInRange.Count; i++)
        {
            //Check the distance of the HookPoint to the player
            float distance = Vector3.Distance(transform.position, hookPointsInRange[i].transform.position) * 1; // Multiply by 1 so its always positive

            //if closestDistance has yet to be assigned, assign this as the closest
            if (closestDistance == 0.0f && distance > minRequiredDistance)
            {
                closestDistance = distance;
                toBeAssigned = hookPointsInRange[i];
            }

            //If this point is closer to the player than our previous closest, assign this as the closest
            if (distance < closestDistance && distance > minRequiredDistance)
            {
                closestDistance = distance;
                toBeAssigned = hookPointsInRange[i];
            }
        }

        return closestDistance;
    }

    private bool VerifyHookPointDistance(HookPoint hp)
    {
        if (Vector3.Distance(hp.transform.position, Camera.main.transform.position) < minRequiredDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // ------ End Non-Unity Methods ------
}
