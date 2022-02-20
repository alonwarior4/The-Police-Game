using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class MainCrimePlace : MonoBehaviour
{
    Sign parentSign;

    private void Start()
    {        
        parentSign = GetComponentInParent<Sign>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {        
        if (otherCollider.TryGetComponent(out CrimeCar otherCrimeCar))
        {
            otherCrimeCar.MainCrimeCollider(parentSign);            
        }
    }
}
