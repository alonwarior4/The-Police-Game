using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSign : MonoBehaviour
{
    [SerializeField] int levelNumber;
    [SerializeField] int signNumber;

    public void OpenRelatedSignAndLevel()
    {
        SelectLevel.stageIsOver(levelNumber, signNumber);
    }
}
