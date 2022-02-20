using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignType
{
    CameraSign , OneWayAlleySign , NoTurningAllowed , NoTurnLeft , NoTurnRight , NoParking 
}

public interface IPlacable
{
    void PlaceItemInPlace();
}

public class Sign : MonoBehaviour , IPlacable 
{
    #region Variables

    [Header("Sign Type")]
    public SignType Type;    

    [Header("Crime Type")]
    [Tooltip("Set Vehicle Crime by Crimetype")] public CrimeType signCrimeType;

    [HideInInspector] public List<CrimeCar> mainCrimeCars = new List<CrimeCar>();
    [HideInInspector] public List<LineRenderer> signLines = new List<LineRenderer>();

    [Header("Laser linerenderer Material")]
    [SerializeField] Material laserMaterial;

    [Header("Start Position of sign line")]
    public Transform lineRendererStartPos;

    [Header("Sign Child Refrences")]
    public GameObject mainColliderObj;
    [SerializeField] SpriteRenderer signSpritePlace;
    [Header("full sign sprite")]
    [SerializeField] Sprite completeSP;

    [Header("MainCrime Defecaulty Configs")]    
    public bool isMainCrimeAvailable;
    public AnimationCurve mainDefecaultyCurve;
    float mainLastWaitTime = 0;
    float mainCurveEvaluateNumber;
    [Header("sign Tapped Cars")]
    [SerializeField] float carsTappedInSignCrime;  

    string laserName = "SignLine ";
    string signLaserLayerName = "SignLaser";

    float signLastKeyIndex;
    //if sign placed or not
    bool isSignPlaced = false;

    //invisible car shield bool anim parameter
    int sheildBoolId => Animator.StringToHash("Shield");

    Coroutine waitingCoroutine;

    #endregion


    private void Awake()
    {
        //just in case
        isSignPlaced = false;
        signLastKeyIndex = mainDefecaultyCurve.keys[mainDefecaultyCurve.keys.Length - 1].time;
    }

    private void Start()
    {
        isMainCrimeAvailable = true;
        //var siblingSigns = transform.parent.GetComponentsInChildren<Sign>();
        //for(int i=0; i< siblingSigns.Length; i++)
        //{
        //    if(siblingSigns[i].Type == Type)
        //    {
        //        maxMyTypeCount++;
        //    }
        //}
    }

    private void Update()
    {
        if (signLines.Count == 0) return;

        for (int i = 0; i < signLines.Count; i++)
        {
            signLines[i].SetPosition(1, mainCrimeCars[i].transform.position);
        }
    }

    public void TapCarInCurrentSign(bool isSpecialCar)
    {
        carsTappedInSignCrime++;
        //print("car tapped num is " + carsTappedInSignCrime + "and last key is " + signLastKeyIndex + name);
        if(carsTappedInSignCrime > signLastKeyIndex)
        {
            isMainCrimeAvailable = false;
        }
        else
        {
            if(isSpecialCar && waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
                isMainCrimeAvailable = false;
            }

            waitingCoroutine =  StartCoroutine(Close_WaitAndReopenSignCoroutine(carsTappedInSignCrime));
        }
    }

    //when normal crime cars , begin main crime work
    public void CheckForOpenSign()
    {
        if(carsTappedInSignCrime > signLastKeyIndex)
        {
            isMainCrimeAvailable = false;
        }
        else
        {
            isMainCrimeAvailable = true;
        }
    }

    public void AddToMainCrimeCars(CrimeCar crimeCar)
    {
        //if (!signSpriteObj.activeInHierarchy) return;
        //CheckSignTutorial();
        if (!isSignPlaced) return;

        AudioManager.AM_Instance.PlaySignLaser();

        GameObject newLineObject = new GameObject( laserName + (transform.childCount - 3), typeof(LineRenderer));
        newLineObject.transform.parent = transform;
        
        LineRenderer newSignLine = newLineObject.GetComponent<LineRenderer>();
        SetLineRendererConfigs(newSignLine);

        mainCrimeCars.Add(crimeCar);
        signLines.Add(newSignLine);

        //if(crimeCar is InvisibleCar invisCar)
        //{
        //    StartCoroutine(DisableLaserAfterTime(invisCar));
        //}
    }

    void SetLineRendererConfigs(LineRenderer lineRenderer)
    {
        // Set Start Laser Position
        lineRenderer.SetPosition(0, lineRendererStartPos.position);

        // line Shape Size
        lineRenderer.startWidth = 0.08f;
        lineRenderer.endWidth = 0.02f;

        // Decrease Shadow Config
        lineRenderer.allowOcclusionWhenDynamic = false;
        lineRenderer.shadowBias = 0;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;

        // Renderer part Config
        lineRenderer.material = laserMaterial;
        lineRenderer.sortingLayerName = signLaserLayerName;
    }

    //IEnumerator DisableLaserAfterTime(InvisibleCar invisibleCar)
    //{
    //    WaitForSeconds waitToLaserTime = new WaitForSeconds(invisibleCar.LaserTime);
    //    yield return waitToLaserTime;

    //    RemoveFromMainCrimeCars(invisibleCar);
    //    invisibleCar.movingObjectAnim.SetBool(sheildBoolId , false);
    //}

    public void RemoveFromMainCrimeCars(CrimeCar crimeCar)
    {        
        if (/*!signSpriteObj.activeInHierarchy*/!isSignPlaced || signLines.Count == 0 || !mainCrimeCars.Contains(crimeCar)) return;

        LineRenderer oldRenderer = signLines[mainCrimeCars.IndexOf(crimeCar)];
        signLines.Remove(oldRenderer);
        mainCrimeCars.Remove(crimeCar);
        Destroy(oldRenderer.gameObject);
    }

    public void PlaceItemInPlace()
    {
        transform.parent.GetComponent<CheckFullSign>().ItemPlaced(Type);
        isSignPlaced = true;
        signSpritePlace.sprite = completeSP;
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator Close_WaitAndReopenSignCoroutine(float tappedCarNumber)
    {
        isMainCrimeAvailable = false;        

        mainCurveEvaluateNumber = mainDefecaultyCurve.Evaluate(tappedCarNumber);
        float waitTime = Mathf.Abs(mainCurveEvaluateNumber - mainLastWaitTime);       
        mainLastWaitTime = mainCurveEvaluateNumber;
        WaitForSeconds waitForWaitTime = new WaitForSeconds(waitTime);
        yield return waitForWaitTime;
        
        isMainCrimeAvailable = true;
    }

    public void CheckSignTutorial(Transform carTransform)
    {
        switch (Type)
        {            
            case SignType.OneWayAlleySign:
                Tutorial.t_Instance.CheckFirstTimeOneWaySignCrime(carTransform);
                break;
            case SignType.NoTurningAllowed:
                Tutorial.t_Instance.CheckFirstTimeNoTurningSignCrime(carTransform);
                break;
            case SignType.NoTurnRight:
                Tutorial.t_Instance.CheckFirstTimeNoTurnRightSignCrime(carTransform);
                break;
            default:
                break;
        }
    }
}
