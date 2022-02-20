using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnPos : MonoBehaviour
{
    //[HideInInspector] public List<Vehicle> inColliderVehilces = new List<Vehicle>();     
    int inColliderVehiclesNumber = 0;

    [Header("Spawn related waypoint")]
    public Waypoint spawnWaypoint;    
    public bool isOutOfOrder = false;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //if (otherCollider.TryGetComponent(out Vehicle otherCar))
        //{
        //    inColliderVehilces.Add(otherCar);
        //    SetSpawnPointState();
        //}
        inColliderVehiclesNumber++;
        SetSpawnPointState();
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.TryGetComponent(out Vehicle otherVehicle))
        {
            if (otherVehicle as SpecialCar)
            {
                //TODO : Test mobile vibrate
                Handheld.Vibrate();
                AudioManager.AM_Instance.PlaySpecEnter();
            }

            //inColliderVehilces.Remove(otherVehicle);
            inColliderVehiclesNumber--;
            SetSpawnPointState();

            //Check for spawn if respawnable car exist
            if (otherVehicle is CrimeCar crimeCar)
                SpawnManager.SM_Intance.RemoveFromRespawnableCars(crimeCar);
        }
    }

    void SetSpawnPointState()
    {
        if(inColliderVehiclesNumber> 0)
        {
            isOutOfOrder = true;
        }
        else
        {
            isOutOfOrder = false;
        }
    }
}
