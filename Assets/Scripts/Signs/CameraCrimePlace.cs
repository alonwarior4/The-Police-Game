using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraCrimePlace : MonoBehaviour
{
    _Camera parentCamera;

    private void Start()
    {
        parentCamera = GetComponentInParent<_Camera>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.TryGetComponent(out NormalCrimeCar other_NCC))
        {
            if (other_NCC.isDoingSomeCrime) return;
            other_NCC.SideCrimeCollider(parentCamera);            
        }
    }
    
}
