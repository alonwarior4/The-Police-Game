using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Police Txt" )]
public class PoliceTextSO : ScriptableObject
{
    [TextArea(3 , 10)]
    public string policeText;
}
