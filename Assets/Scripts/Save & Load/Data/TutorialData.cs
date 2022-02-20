

[System.Serializable]
public class TutorialData : SaveData
{
    //default
    public bool isFirstTimeDefault;

    //first time crimes
    public bool isFirstTimeCarCrime , isFirstTimeCarSideCrime , isFirstTimeSignMoney , isFirstTimeCameraMoney;

    //first time special cars
    public bool isFirstTimeNoTapCar , isFirstTimeMultiTapCar /*, isFirstTimeInvisCar*/;

    //first time sign crime
    public bool isFirstTimeOneWaySign , isFirstTimeNoTurnRight , isFirstTimeNoTurning;

    public TutorialData(Tutorial tutorial) : base (tutorial)
    {
        isFirstTimeDefault = tutorial.isFirstTimeDefaultTime;

        isFirstTimeCarCrime = tutorial.isFirstTimeCarCrime;
        isFirstTimeCarSideCrime = tutorial.isFirstTimeCarSideCrime;
        isFirstTimeSignMoney = tutorial.isFirstTimeSignMoney;
        isFirstTimeCameraMoney = tutorial.isFirstTimeCameraMoney;

        isFirstTimeNoTapCar = tutorial.isFirstTimeNoTapCar;
        isFirstTimeMultiTapCar = tutorial.isFirstTimeMultiTapCar;
        //isFirstTimeInvisCar = tutorial.isFirstTimeInvisCar;

        isFirstTimeOneWaySign = tutorial.isFirstTimeOneWaySign;
        isFirstTimeNoTurnRight = tutorial.isFirstTimeNoTurnRight;
        isFirstTimeNoTurning = tutorial.isFirstTimeNoTurning;
    }
    
}
