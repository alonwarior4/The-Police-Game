using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    //TODO : be sure to initialize list with  capacity 
    public static SpawnManager SM_Intance;

    /*[HideInInspector]*/ public List<CrimeCar> respawnableCars = new List<CrimeCar>();

    [Header("spawn positions in scene")]
    [SerializeField] SpawnPos[] spawnPositions;
    List<SpawnPos> openSpawnPos = new List<SpawnPos>();




    private void Awake()
    {
        if(SM_Intance == null)
        {
            SM_Intance = this;
        }
    }

    public void AddToRespawnableCars(CrimeCar crimeCar)
    {
        respawnableCars.Add(crimeCar);
        //CrimeCar carToRespawn;

        if (SpecialCarManager.SCM_Instance.isSpecialCarInQueue)
        {
            //carToRespawn = SpecialCarManager.SCM_Instance.selectedSpecialCar;
            CheckForSpawn(SpecialCarManager.SCM_Instance.selectedSpecialCar);
        }
        else
        {
            //carToRespawn = respawnableCars[0];
            if(respawnableCars.Count > 0)
            {
                CheckForSpawn(respawnableCars[0]);
            }
        }

        //CheckForSpawn(carToRespawn);
    }

    public void RemoveFromRespawnableCars(CrimeCar crimeCar)
    {
        if (respawnableCars.Contains(crimeCar))
        {
            //print("is removed from respawnable cars " + crimeCar.name);
            respawnableCars.Remove(crimeCar);
        }
        //CrimeCar carToSpawn = null;
        if (SpecialCarManager.SCM_Instance.isSpecialCarInQueue)
        {
            //print("there is special car in queue");
            //carToSpawn = SpecialCarManager.SCM_Instance.selectedSpecialCar;
            CheckForSpawn(SpecialCarManager.SCM_Instance.selectedSpecialCar);
        }
        else
        {
            //print("there is no special car in queue");
            if (respawnableCars.Count > 0)
            {
                //print("selected first car in respawnable car to spawn " + respawnableCars[0].name);
                //carToSpawn = respawnableCars[0];
                CheckForSpawn(respawnableCars[0]);
            }
        }

        //if (carToSpawn != null)
        //{
        //    CheckForSpawn(carToSpawn);
        //}
    }

    public void CheckForSpawn(CrimeCar crimeCar)
    {        
        SpawnPos openPos = SelectOpenSpawnPosition();
        if (openPos == null) return;

        crimeCar.CurrentCrime = new NoCrime(crimeCar);

        crimeCar.currentSign = null;
        crimeCar.isDoingSomeCrime = false;
        crimeCar.isCanTouch = true;
        crimeCar.carCollider.enabled = true;
        crimeCar.transform.position = openPos.transform.position;
        crimeCar.currentWaypoint = openPos.spawnWaypoint;

        if(crimeCar is SpecialCar specCar)
        {
            SpecialCarManager.SCM_Instance.isSpecialCarInQueue = false;
            SpecialCarManager.SCM_Instance.isSpecialCarInScene = true;

            specCar.pathIndex = 0;
            specCar.DoSomeThingBeforeResapwn();
            specCar.FindWaypointPath();
        }
        else if(crimeCar is NormalCrimeCar normalCrimeCar)
        {
            normalCrimeCar.SetNextWaypoint();            
        }

        crimeCar.SetDestination();
        crimeCar.SetMoveDirection();
        crimeCar.ChangeAnimState();       

        crimeCar.moveSM.ChangeState(crimeCar.movingState);     
    }

    SpawnPos SelectOpenSpawnPosition()
    {
        for(int i=0; i< spawnPositions.Length; i++)
        {
            if(spawnPositions[i].isOutOfOrder == false)
            {
                openSpawnPos.Add(spawnPositions[i]);
            }       
        }

        if(openSpawnPos.Count > 0)
        {
            int randomNum = Random.Range(0, openSpawnPos.Count);            
            return (openSpawnPos[randomNum]);
        }
        else
        {            
            return null;
        }
    }

    private void OnDisable()
    {
        SM_Intance = null;
    }
}
