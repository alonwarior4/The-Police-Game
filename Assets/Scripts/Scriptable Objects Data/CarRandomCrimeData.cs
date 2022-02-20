using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Crime Sprite Data" , fileName = "Sprite Data")]
public class CarRandomCrimeData : ScriptableObject
{
    public Sprite[] randomCrimePopups;

    public Sprite GetRandomSpriteFromList()
    {
        return randomCrimePopups[Random.Range(0, randomCrimePopups.Length)];
    } 
}