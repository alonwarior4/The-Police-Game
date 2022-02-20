using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public SignType signTypeToPlace;
    IPlacable placable;

    private void Awake()
    {
        placable = GetComponent<IPlacable>();
    }

    public void EnableItemInPlace()
    {
        //IPlacable placable = GetComponent<IPlacable>();
        placable.PlaceItemInPlace();
    }
}
