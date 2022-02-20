using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Azhir : MonoBehaviour
{
    public static Azhir azhir_Instance;
    int momentCrimeCars;
    [SerializeField] GameObject azhirObjPrefab;
    SpriteRenderer azhirSprite;
    GameObject azhirObjRef;    



    private void Awake()
    {
        if (!azhir_Instance)
        {
            azhir_Instance = this;
        }

        azhirObjRef = Instantiate(azhirObjPrefab , Vector3.zero , Quaternion.identity);
    }

    private void Start()
    {
        momentCrimeCars = 0;        
        azhirSprite = azhirObjRef.GetComponent<SpriteRenderer>();
    }

    public void Azhir_AddToMomentCrimeCars()
    {
        momentCrimeCars++;
        PlayAzhir();
    }

    public void Azhir_RemoveFromMomentCrimeCars()
    {
        momentCrimeCars--;
        if(momentCrimeCars == 0)
        {
            PauseAzhir();
        }
    }

    void PlayAzhir()
    {
        azhirSprite.enabled = true;
        AudioManager.AM_Instance.PlayAzhir();
    }

    public void PauseAzhir()
    {
        azhirSprite.enabled = false;
    }

    public void ChangeAzhirAnimObjPostion(Vector3 newPosition)
    {
        newPosition.z = 0;
        azhirObjRef.transform.position = newPosition;
    }

    private void OnDestroy()
    {
        azhir_Instance = null;                                   //free static instance memory
    }
}
