using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BolBolUtility;


public class Waypoint : MonoBehaviour, IBFSable<Waypoint>
{
    [HideInInspector] public nextWaypointLines currentLine;

    [Header("Waypoint specific Id")]
    public int wayPointId;

    [Header("Waypoint Next Lines")]
    public List<nextWaypointLines> nextLines = new List<nextWaypointLines>();

    [Header("Previous Waypoints")]
    public List<Waypoint> previousWaypoints = new List<Waypoint>();

    public List<Waypoint> nextPoints { get; set; }

    List<nextWaypointLines> nextRightLines = new List<nextWaypointLines>(); 
    List<nextWaypointLines> nextWrongLines = new List<nextWaypointLines>();    
    
    

    private void Awake()
    {
        nextPoints = new List<Waypoint>();
        for (int i = 0; i < nextLines.Count; i++)
        {
            nextPoints.Add(nextLines[i].nextWaypoint);
            if (nextLines[i].IsWrongWay)
            {
                nextWrongLines.Add(nextLines[i]);
            }
            else
            {
                nextRightLines.Add(nextLines[i]);
            }
        }
    }

    public Waypoint _GetNextRightWaypoint()
    {   
        return SelectRightLineWithChance()?.nextWaypoint;
    }

    public Waypoint _GetNextWrongWaypoint()
    {
        return SelectWrongLineWithChance()?.nextWaypoint;
    }

    private nextWaypointLines SelectRightLineWithChance()
    {
        if (nextRightLines.Count == 1)
        {
            return nextRightLines[0];
        }
        else
        {
            float randomNum = UnityEngine.Random.Range(0f, 1f);

            float[] minOffsets = new float[nextRightLines.Count];
            float[] maxOffsets = new float[nextRightLines.Count];

            for (int i = 0; i < nextRightLines.Count; i++)
            {
                if (i == 0)
                {
                    minOffsets[i] = 0;
                    maxOffsets[i] = nextRightLines[i].waypointChance;
                }
                else
                {
                    minOffsets[i] = maxOffsets[i - 1];
                    maxOffsets[i] = maxOffsets[i - 1] + nextRightLines[i].waypointChance;
                }


                if (randomNum > minOffsets[i] && randomNum < maxOffsets[i])
                {
                    return nextRightLines[i];
                }
            }

            return default;
        }
    }

    private nextWaypointLines SelectWrongLineWithChance()
    {
        if (nextWrongLines.Count == 1)
        {
            return nextWrongLines[0];
        }
        else
        {
            float randomNum = UnityEngine.Random.Range(0f, 1f);

            float[] minOffsets = new float[nextWrongLines.Count];
            float[] maxOffsets = new float[nextWrongLines.Count];

            for (int i = 0; i < nextWrongLines.Count; i++)
            {
                if (i == 0)
                {
                    minOffsets[i] = 0;
                    maxOffsets[i] = nextWrongLines[i].waypointChance;
                }
                else
                {
                    minOffsets[i] = maxOffsets[i - 1];
                    maxOffsets[i] = maxOffsets[i - 1] + nextWrongLines[i].waypointChance;
                }


                if (randomNum > minOffsets[i] && randomNum < maxOffsets[i])
                {
                    return nextWrongLines[i];
                }
            }

            return default;
        }
    }

    public static nextWaypointLines GetLine(Waypoint first , Waypoint second)
    {     
        List<nextWaypointLines> f_nextLines = first.nextLines;
        for(int i=0; i<f_nextLines.Count; i++)
        {
            if(f_nextLines[i].nextWaypoint == second)
            {
                return f_nextLines[i];
            }
        }

        return default;
    }
}



[System.Serializable]
public class nextWaypointLines
{
    [Header("Line Waypoint")]
    public Waypoint nextWaypoint;

    [Header("Green right , red wrong")]
    public bool IsWrongWay;

    [Header("Chance to be selected")]
    [Range(0f, 1f)]
    public float waypointChance;
}

