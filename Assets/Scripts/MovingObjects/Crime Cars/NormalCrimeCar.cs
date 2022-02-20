using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCrimeCar : CrimeCar
{
    [SerializeField] float sidePunishValue;
    public float side_TapAllowedTime;

    [Header("Chance to Do Sign crime")]
    [Range(0f, 1f)] public float sideCrimeChance;

    [Header("random Crime Config")]
    public CarRandomCrimeData randomSprites;
    public SpriteRenderer randomSpritePlace;
    public Animator popUpAnim;

    [HideInInspector] public _Camera currentCamera;
    //AudioSource carAS;
    [SerializeField] AudioClip popUpSound;
    public AudioClip popUpTapSound;



    protected override void Awake()
    {
        base.Awake();
        //carAS = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        SetNextWaypoint();
        base.Start();
    }

    public override void SetNextWaypoint()
    {
        if (isDoingSomeCrime && currentSign != null)
        {
            nextWaypoint = currentWaypoint._GetNextWrongWaypoint();
        }
        else
        {
            nextWaypoint = currentWaypoint._GetNextRightWaypoint();
        }
    }

    //when collide with main crime collider
    public override void MainCrimeCollider(Sign sign)
    {
        if (isDoingSomeCrime || sign.isMainCrimeAvailable == false || IsNextPointHasIssue()) return;

        float randomNum = Random.Range(0f, 1f);
        if (randomNum <= mainCrimeChance)
        {
            nextWaypoint = currentWaypoint._GetNextWrongWaypoint();
            currentSign = sign;
            currentSign.CheckSignTutorial(transform);
            currentSign.AddToMainCrimeCars(this);
            CurrentCrime = CrimeManager.GetCrimeByCrimeType(currentSign.signCrimeType, this);
            Tutorial.t_Instance.CheckFirstTimeCarCrime(transform);
        }
    }
    
    //check not to "sarotah" crime car
    bool IsNextPointHasIssue()
    {
        Waypoint nextWaypoint = currentWaypoint._GetNextWrongWaypoint();
        Vector3 currentDir = moveDirection.normalized;
        Vector3 newDir = (nextWaypoint.transform.position - transform.position).normalized;
        return Vector3.Dot(newDir, currentDir) < -0.65f;
    }

    //when collide with side crime collider
    public void SideCrimeCollider(_Camera camera)
    {
        //carAS.PlayOneShot(popUpSound);        

        float randomNum = Random.Range(0f, 1f);
        if (randomNum <= sideCrimeChance)
        {
            Tutorial.t_Instance.CheckFirstTimeSideCrime(transform);
            currentCamera = camera;
            CurrentCrime = new RandomCrime(this);
            camera.SetColliderByCurve();
        }
    }

    public void SidePunish() { GameManager.GM_Instance.TotalMoney += sidePunishValue; }

}
