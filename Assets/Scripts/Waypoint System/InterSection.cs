using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManageSystem
{
    Waypoint , Direction , Mixed
}

public class InterSection : MonoBehaviour
{
    [SerializeField] ManageSystem manageSystem = ManageSystem.Waypoint;
    public List<Vehicle> enteringVehicles = new List<Vehicle>();

    [Header("Intersection related Waypoint")]
    public List<Waypoint> relatedWaypoint;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.TryGetComponent(out Vehicle otherVehicle))
        {
            if (manageSystem == ManageSystem.Waypoint)
            {
                if (relatedWaypoint.Contains(otherVehicle.currentWaypoint) || relatedWaypoint.Contains(otherVehicle.nextWaypoint))
                {
                    enteringVehicles.Add(otherVehicle);
                    if (enteringVehicles.Count > 1)
                    {
                        otherVehicle.moveSM.ChangeState(otherVehicle.stopingState);
                    }
                }
            }
            else if (manageSystem == ManageSystem.Direction)
            {
                Vector3 vehicleMoveDirection = otherVehicle.moveDirection.normalized;
                Vector3 intersectionDirection = (transform.position - otherVehicle.transform.position).normalized;
                if (Vector3.Dot(vehicleMoveDirection, intersectionDirection) > 0.85f)
                {
                    enteringVehicles.Add(otherVehicle);
                    if (enteringVehicles.Count > 1)
                    {
                        otherVehicle.moveSM.ChangeState(otherVehicle.stopingState);
                    }
                }
            }
            else if (manageSystem == ManageSystem.Mixed)
            {
                if (relatedWaypoint.Contains(otherVehicle.currentWaypoint) || relatedWaypoint.Contains(otherVehicle.nextWaypoint))
                {
                    enteringVehicles.Add(otherVehicle);
                }
                else
                {
                    Vector3 vehicleMoveDirection = otherVehicle.moveDirection.normalized;
                    Vector3 intersectionDirection = (transform.position - otherVehicle.transform.position).normalized;
                    if (Vector3.Dot(vehicleMoveDirection, intersectionDirection) > 0.85f)
                    {
                        enteringVehicles.Add(otherVehicle);
                    }
                }

                if (enteringVehicles.Count > 1)
                {
                    otherVehicle.moveSM.ChangeState(otherVehicle.stopingState);
                    //otherVehicle.stopCause = "intersection made me do it";
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if(otherCollider.TryGetComponent(out Vehicle otherVehicle))
        {            
            if (enteringVehicles.Contains(otherVehicle))
            {
                enteringVehicles.Remove(otherVehicle);
                if (enteringVehicles.Count > 0)
                {
                    enteringVehicles[0].moveSM.ChangeState(enteringVehicles[0].movingState);
                    //exitingCar = enteringVehicles[0];
                }
            }           
        }
    }
}
