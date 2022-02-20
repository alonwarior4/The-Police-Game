using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Item : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler, IMoneyAffectable
{
    public SignType itemType;
    ContinueTutoActionState tutoState;

    [Header("Coin need to buy")]
    public float value;

    [Header("item Value Refrence")]
    [SerializeField] GameObject itemValue;
    Image itemSprite;
    Image itemValueBGPlace;
    Text valueNumBlueBgPlace;

    [SerializeField] Sprite blueValueBG;
    [SerializeField] Sprite grayValueBG;
    [SerializeField] Color numBlueColor;
    [SerializeField] Color numGrayColor;

    GameObject dragableSign;
    Camera mainCamera;

    string dragableSignSortingLayerName = "D_Sign";
    string draggableName = "Ds";
    bool isInteractable = false;

    //TODO : Check if array size is enough
    RaycastHit2D[] rayHit = new RaycastHit2D[2];

    GamePlayUiBtn uiManager;
    int signLayerMask = 1 << 10;

    bool isCanPlayOpenSound;

    float currentTimeScale;



    private void Awake()
    {
        //isInteractable = true;
        uiManager = GetComponentInParent<GamePlayUiBtn>();
        itemSprite = transform.GetChild(0).GetComponent<Image>();
        itemValueBGPlace = itemValue.GetComponent<Image>();
        valueNumBlueBgPlace = itemValue.transform.GetChild(0).GetComponent<Text>();
        foreach (Transform child in itemValue.transform)
        {
            child.GetComponent<Text>().text = value.ToString();
        }
        SetContinueState();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EventBroker.AddOnMoneyChangeObserver(this);
    }

    public void OnMoneyChangeNotify(float totalMoney)
    {
        SetSliderValue(totalMoney);
    }

    void SetSliderValue(float coinValue)
    {
        if(coinValue >= value)
        {
            if (itemType == SignType.CameraSign)
                Tutorial.t_Instance.CheckFirstTimeCameraMoney(transform.position);
            else
                Tutorial.t_Instance.CheckFirstTimeSignMoney(transform.position);

            itemValueBGPlace.sprite = blueValueBG;
            valueNumBlueBgPlace.color = numBlueColor;
            isInteractable = true;

            if (isCanPlayOpenSound)
            {
                AudioManager.AM_Instance.PlayItemOpenSound();
                isCanPlayOpenSound = false;
            }
        }
        else
        {
            itemValueBGPlace.sprite = grayValueBG;
            valueNumBlueBgPlace.color = numGrayColor;
            isInteractable = false;

            isCanPlayOpenSound = true;
        }
    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;
        //print("starting draging");

        AudioManager.AM_Instance.PlayItemPick();

        CreateDragableSign();
        currentTimeScale = Time.timeScale;
        Time.timeScale = GameManager.GM_Instance.timeScaleWhenDragSign;
        //uiManager.CloseInventoryPanel();
    }

    void CreateDragableSign()
    {
        //TODO : Object pool draggable sign
        //print("creating dragging item");
        dragableSign = new GameObject(draggableName, typeof(SpriteRenderer));
        dragableSign.transform.localScale *= 0.89f;
        SpriteRenderer SR = dragableSign.GetComponent<SpriteRenderer>();
        SR.sprite = itemSprite.sprite;
        SR.sortingLayerName = dragableSignSortingLayerName;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;

        //print("dragging");
        Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        touchWorldPos.z = 0;
        dragableSign.transform.position = touchWorldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;

        //print("on end drag");
        //Time.timeScale = 1;
        Time.timeScale = currentTimeScale;
        Vector3 touchPos = mainCamera.ScreenToWorldPoint(eventData.position);
        Destroy(dragableSign);
        //uiManager.CloseInventoryPanel();

        

        //RaycastHit2D rayHit = Physics2D.Raycast(touchPos , mainCamera.transform.forward);

        //if(rayHit && rayHit.transform.GetComponent<ItemPlace>())
        //{
        //    ItemPlace itemPlace = rayHit.transform.GetComponent<ItemPlace>();
        //    if(itemPlace.signTypeToPlace == itemType)
        //    {
        //        EndDragSignInTruePlace(itemPlace);
        //    }
        //}

        //TODO : check the refactor func below if its working
        if (Physics2D.RaycastNonAlloc(touchPos , mainCamera.transform.forward , rayHit , 10 , signLayerMask) > 0 &&
            rayHit[0].transform.TryGetComponent(out ItemPlace itemPlace) &&
            itemPlace.signTypeToPlace == itemType)
        {
            //print("diagnosing sign place");
            EndDragSignInTruePlace(ref itemPlace);
        }
        else
        {
            AudioManager.AM_Instance.PlayWrongItemPlacement();
        }
    }

    public void EndDragSignInTruePlace(ref ItemPlace place)
    {
        Time.timeScale = 1;
        Tutorial.t_Instance.CheckContinueState(tutoState);
        GameManager.GM_Instance.TotalMoney -= value;
        AudioManager.AM_Instance.PlayItemDrop();
        place.EnableItemInPlace();

        //dragableSign = null;
    }

    private void OnDisable()
    {
        EventBroker.RemoveMoneyChangeObserver(this);
    }
    
    void SetContinueState()
    {
        switch (itemType)
        {
            case SignType.CameraSign:
                tutoState = ContinueTutoActionState.placeCamera;
                break;
            case SignType.OneWayAlleySign:
                tutoState = ContinueTutoActionState.PlaceOneWaySign;
                break;
            case SignType.NoTurningAllowed:
                tutoState = ContinueTutoActionState.PlaceNoTurningAllowedSign;
                break;
            case SignType.NoTurnLeft:
                tutoState = ContinueTutoActionState.PlaceNoTurnLeftSign;
                break;
            case SignType.NoTurnRight:
                tutoState = ContinueTutoActionState.PlaceNoTurnRightSign;
                break;
            case SignType.NoParking:
                tutoState = ContinueTutoActionState.placeNoParkingSign;
                break;
            default:
                break;
        }
    }
}
