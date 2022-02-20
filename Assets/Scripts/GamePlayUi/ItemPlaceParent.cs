using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlaceParent : MonoBehaviour
{
    List<Image> SignImages = new List<Image>();

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            SignImages.Add(child.GetChild(0).GetComponent<Image>());
        }  
    }

    private void Start()
    {
        DisableItems();
    }

    public void EnableItems()
    {
        for(int i=0; i< SignImages.Count; i++)
        {
            SignImages[i].raycastTarget = true;
        }
    }

    public void DisableItems()
    {
        for (int i = 0; i < SignImages.Count; i++)
        {
            SignImages[i].raycastTarget = false;
        }
    }
}
