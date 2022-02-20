using UnityEngine;


public abstract class Crime
{
    protected Coroutine CrimeCoroutine;
    protected CrimeCar targetCrimeCar;
    public Crime(CrimeCar crimeCar)
    {
        targetCrimeCar = crimeCar;
        targetCrimeCar.TouchWork = TouchWork;
    }

    //work to do when doing crime
    public abstract void CrimeWork();

    //work to do when touch
    public abstract void TouchWork();
}
