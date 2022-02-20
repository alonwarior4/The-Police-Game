using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class _Camera : MonoBehaviour , IPlacable
{
    [Header("Camera parts")]
    [SerializeField] SpriteRenderer cameraSpritePlace;
    public GameObject cameraCollider;

    [Header("Camera Crime Defecaulty Config")]
    public AnimationCurve sideDefecaultyCurve;
    [HideInInspector] public float sideLastWaitTime;
    [HideInInspector] public float sideCurveEvaluateNumber;

    [SerializeField] Sprite completeSp;



    float sideCrimeNumber = 1;
    public float SideCrimeNumber
    {
        get
        {
            return sideCrimeNumber;
        }
        set
        {
            sideCrimeNumber = value;
            sideLastWaitTime = sideCurveEvaluateNumber;
        }
    }

    int cameraLastCurveIndex ;

    private void Awake()
    {
        cameraLastCurveIndex = (int)sideDefecaultyCurve.keys[sideDefecaultyCurve.keys.Length - 1].time;
    }

    public void PlaceItemInPlace()
    {
        //cameraSpritePlace.SetActive(true);
        transform.parent.GetComponent<CheckFullSign>().ItemPlaced(SignType.CameraSign);
        cameraSpritePlace.sprite = completeSp;
        cameraCollider.SetActive(true);

        Destroy(GetComponent<Collider2D>());
        //Destroy(this);
    }

    public void SetColliderByCurve()
    {
        if (SideCrimeNumber > /*sideDefecaultyCurve.keys[sideDefecaultyCurve.keys.Length - 1].time*/ cameraLastCurveIndex)
        {
            cameraCollider.SetActive(false);
        }
        else
        {
            StartCoroutine(SetColliderByCurveCoroutine());
        }
    }

    IEnumerator SetColliderByCurveCoroutine()
    {
        cameraCollider.SetActive(false);

        sideCurveEvaluateNumber = sideDefecaultyCurve.Evaluate(SideCrimeNumber);
        float waitTime = Mathf.Abs(sideCurveEvaluateNumber - sideLastWaitTime);

        WaitForSeconds waitForWaitTime = new WaitForSeconds(waitTime);
        yield return waitForWaitTime;

        cameraCollider.SetActive(true);
    }

}
