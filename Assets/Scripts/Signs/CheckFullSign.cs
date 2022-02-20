using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFullSign : MonoBehaviour
{
    //maximum types of signs in scene
    int maxOneWayAlleyCount;
    int maxUTurnCount;
    int maxNoTurnLeftCount;
    int maxNoTurnRightCount;
    int maxNoParkingCount;
    int maxCameraCount;

    //current types of signs
    int oneWayAlleyCount;
    int uTurnCount;
    int noTurnLeftCount;
    int noTurnRightCount;
    int noParkingCount;
    int cameraCount;

    BotUiPanel botUiPanel;




    private void Start()
    {
        botUiPanel = FindObjectOfType<BotUiPanel>();

        foreach(Transform child in transform)
        {           
            SignType type = child.GetComponent<ItemPlace>().signTypeToPlace;

            switch (type)
            {
                case SignType.CameraSign:
                    maxCameraCount++;
                    break;
                case SignType.OneWayAlleySign:
                    maxOneWayAlleyCount++;
                    break;
                case SignType.NoTurningAllowed:
                    maxUTurnCount++;
                    break;
                case SignType.NoTurnLeft:
                    maxNoTurnLeftCount++;
                    break;
                case SignType.NoTurnRight:
                    maxNoTurnRightCount++;
                    break;
                case SignType.NoParking:
                    maxNoParkingCount++;
                    break;
                default:
                    break;
            }
        }
    }

    public void ItemPlaced(SignType placedItemType)
    {
        switch (placedItemType)
        {
            case SignType.CameraSign:
                cameraCount++;
                break;
            case SignType.OneWayAlleySign:
                oneWayAlleyCount++;
                break;
            case SignType.NoTurningAllowed:
                uTurnCount++;
                break;
            case SignType.NoTurnLeft:
                noTurnLeftCount++;
                break;
            case SignType.NoTurnRight:
                noTurnRightCount++;
                break;
            case SignType.NoParking:
                noParkingCount++;
                break;
            default:
                break;
        }

        CheckToCloseInventory();
    }

    void CheckToCloseInventory()
    {
        if (cameraCount == maxCameraCount && oneWayAlleyCount == maxOneWayAlleyCount && uTurnCount == maxUTurnCount &&
            noTurnLeftCount == maxNoTurnLeftCount && noTurnRightCount == maxNoTurnRightCount && 
            noParkingCount == maxNoParkingCount)
        {
            botUiPanel.CloseInventoryPanel();
        }
    }    
}
