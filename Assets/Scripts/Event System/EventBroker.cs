using System;
using System.Collections;
using System.Collections.Generic;


public static class EventBroker 
{
    #region On Money Change Event

    static List<IMoneyAffectable> OnMoneyChangeList = new List<IMoneyAffectable>();
    public static void AddOnMoneyChangeObserver(IMoneyAffectable moneyAffected)
    {
        OnMoneyChangeList.Add(moneyAffected);
    }

    public static void RemoveMoneyChangeObserver(IMoneyAffectable moneyAffected)
    {
        OnMoneyChangeList.Remove(moneyAffected);
    }

    public static void OnMoneyChange(float totalMoney)
    {
        if (OnMoneyChangeList.Count == 0) return;
        for(int i=0; i< OnMoneyChangeList.Count; i++)
        {
            OnMoneyChangeList[i].OnMoneyChangeNotify(totalMoney);
        }
    }

    #endregion

    #region On Tapped Car Event

    static List<ITappedCarAffectable> OnTappedCarList = new List<ITappedCarAffectable>();

    public static void AddTappedCarObserver(ITappedCarAffectable tappedCarAffectable)
    {
        OnTappedCarList.Add(tappedCarAffectable);
    }

    public static void RemoveTappedCarObserver(ITappedCarAffectable tappedCarAffectable)
    {
        OnTappedCarList.Remove(tappedCarAffectable);
    }

    public static void OnTappedCar(float tappedCars)
    {
        if (OnTappedCarList.Count == 0) return;
        for(int i=0; i< OnTappedCarList.Count; i++)
        {
            OnTappedCarList[i].OnTappedCarNotify(tappedCars);
        }
    }

    #endregion

}



public interface IMoneyAffectable
{
    void OnMoneyChangeNotify(float totalMoney);
}

public interface ITappedCarAffectable
{
    void OnTappedCarNotify(float tappedCars);
}