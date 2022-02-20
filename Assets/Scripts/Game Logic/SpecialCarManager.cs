using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialCarManager : MonoBehaviour , ITappedCarAffectable
{
    public static SpecialCarManager SCM_Instance;

    [Header("true if special car in scene")]
    public bool isSpecialCarInScene;
    [Header("true if special car in spawn queue")]
    public bool isSpecialCarInQueue;

    [Header("Curve to evalute Special Logic")]
    [SerializeField] AnimationCurve LogicCurve;

    [Header("Multiple Pathes To Choose")]
    public SpecialPath[] specialPathes;

    [Header("All Special Cars Available")]
    [SerializeField] Chance_SpecCar[] specialCars;

    [HideInInspector] public SpecialCar selectedSpecialCar; 


    private void Awake()
    {
        if (!SCM_Instance)
        {
            SCM_Instance = this;
        }
    }

    private void OnEnable()
    {
        EventBroker.AddTappedCarObserver(this);
    }    

    public void OnTappedCarNotify(float tappedCarsNumber)
    {
        if (tappedCarsNumber > LogicCurve.keys[LogicCurve.keys.Length - 1].time) return;

        //TODO : delete below print to not create garbage
        //print("tapped car number passed to special curve is " + tappedCarsNumber);

        if (specialCars.Length == 0 || isSpecialCarInScene) return;
        float randomNum = Random.Range(0f, 100f);

        float curveEvaluate = LogicCurve.Evaluate(tappedCarsNumber);
        if (randomNum <= curveEvaluate)
        {
            isSpecialCarInQueue = true;
            //selectedSpecialCar = specialCars[Random.Range(0, specialCars.Length)];            
            selectedSpecialCar = SelectSpecialWithChance();
            SpawnManager.SM_Intance.CheckForSpawn(selectedSpecialCar);
        }
    }

    private void OnDisable()
    {
        SCM_Instance = null;
    }

    private void OnDestroy()
    {
        EventBroker.RemoveTappedCarObserver(this);
    }

    public SpecialPath GetSpecialPath(Waypoint currentSpawnedWaypoint)
    {
        List<SpecialPath> selectedPathes = new List<SpecialPath>();
        for(int i=0; i< specialPathes.Length; i++)
        {
            if(specialPathes[i].pathPoints[0] == currentSpawnedWaypoint)
            {
                selectedPathes.Add(specialPathes[i]);
            }
        }

        if(selectedPathes.Count > 0)
        {
            return selectedPathes[Random.Range(0, selectedPathes.Count - 1)];
        }
        else
        {
            return null;
        }
    }


    SpecialCar SelectSpecialWithChance()
    {
        if (specialCars.Length == 1)
        {
            return specialCars[0].specialCar;
        }
        else
        {
            float randomNum = UnityEngine.Random.Range(0f, 1f);

            float[] minOffsets = new float[specialCars.Length];
            float[] maxOffsets = new float[specialCars.Length];

            for (int i = 0; i < specialCars.Length ; i++)
            {
                if (i == 0)
                {
                    minOffsets[i] = 0;
                    maxOffsets[i] = specialCars[i].chance;
                }
                else
                {
                    minOffsets[i] = maxOffsets[i - 1];
                    maxOffsets[i] = maxOffsets[i - 1] + specialCars[i].chance;
                }


                if (randomNum > minOffsets[i] && randomNum < maxOffsets[i])
                {
                    return specialCars[i].specialCar;
                }
            }

            return default;
        }
    }
}



[System.Serializable]
public class SpecialPath
{
    public List<Waypoint> pathPoints;
}


[System.Serializable]
public class Chance_SpecCar
{
    public SpecialCar specialCar;
    [Range(0f, 1f)] public float chance;
}