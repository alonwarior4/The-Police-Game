using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionUpdater 
{
    static string objName = "Function Updater";
    public class MonoUpdater : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            onUpdate?.Invoke();   
        }
    }

    public static void Create(Func<bool> updaterFunc , float timeStep , Action onCancelAction)
    {
        GameObject updaterObject = new GameObject(objName, typeof(MonoUpdater));
        MonoUpdater updater = updaterObject.GetComponent<MonoUpdater>();
        FunctionUpdater functionUpdater = new FunctionUpdater(updaterObject, updaterFunc, timeStep , onCancelAction);
        updater.onUpdate = functionUpdater.Update;
    }

    Func<bool> updaterFunc;
    Action onCancelAction;
    float timeStep;
    GameObject updaterObj;
    float timePassed;


    public FunctionUpdater( GameObject updaterObj , Func<bool> updaterFunc , float timeStep , Action onCancelAction)
    {
        this.updaterFunc = updaterFunc;
        this.timeStep = timeStep;        
        this.onCancelAction = onCancelAction;
        this.updaterObj = updaterObj;
    }

    public void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= timeStep)
        {
            timePassed = 0;
            if (updaterFunc())
            {
                DestroySelf();
            }
        }        
    }

    internal static float Create(bool v1, float v2, Action moveNextCar)
    {
        throw new NotImplementedException();
    }

    void DestroySelf()
    {
        Debug.Log("destroying self");
        onCancelAction?.Invoke();
        if(updaterObj != null)
        {
            UnityEngine.Object.Destroy(updaterObj);
        }
    }
    
}
