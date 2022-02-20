using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecEnterTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.TryGetComponent(out SpecialCar specCar))
        {
            specCar.CheckSpecTutorial();
        }
    }
}
