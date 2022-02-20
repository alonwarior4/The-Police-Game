using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BolBolUtility
{
    public interface IBFSable<T>
    {
        List<T> nextPoints { get; set; }
    }

    namespace BolBolAI
    {
        public class AITools
        {
            public static List<T> PathFind<T>(T startPoint , T endPoint) where T : IBFSable<T>
            {
                List<T> Path = new List<T>();
                Queue<T> pointsQueue = new Queue<T>();
                Dictionary<T, T> pointsRelative = new Dictionary<T, T>();
                List<T> usedPoints = new List<T>();

                pointsQueue.Enqueue(startPoint);
                while(pointsQueue.Count > 0)
                {
                    T searchedOne = pointsQueue.Dequeue();
                    usedPoints.Add(searchedOne);
                    if(searchedOne.Equals(endPoint))
                    {
                        break;
                    }

                    for(int i=0; i< searchedOne.nextPoints.Count; i++)
                    {
                        if(!usedPoints.Contains(searchedOne.nextPoints[i]))
                        {
                            usedPoints.Add(searchedOne.nextPoints[i]);
                            pointsRelative.Add(searchedOne.nextPoints[i] , searchedOne);
                            pointsQueue.Enqueue(searchedOne.nextPoints[i]);
                        }
                    }
                }

                Path.Add(endPoint);
                T previousePoint = endPoint;
                if (pointsRelative.TryGetValue(previousePoint, out T value) == false) return null;

                while (!previousePoint.Equals(startPoint))
                {
                    Path.Add(pointsRelative[previousePoint]);
                    previousePoint = pointsRelative[previousePoint];
                }

                Path.Reverse();
#if UNITY_EDITOR
                string pathString = "";
                for (int i = 0; i < Path.Count; i++)
                {
                    string waypointName = Path[i].ToString();
                    waypointName.Remove(0, 8);
                    pathString += waypointName + " , ";
                }
                Debug.Log(pathString);
#endif
                return Path;
            }
        }
    }
}
