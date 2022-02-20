using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;


    public AudioClip[] sounds;
    public AudioSource PlaySounds;



    private void Awake()
    {
        instance = this;
    }


    private void OnDestroy()
    {
        Destroy(instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        MusicChanger();

    }



    // Update is called once per frame
    



    public bool sound=true;
    public void ClickBtn()
    {
        if (sound)
        {
            PlaySounds.PlayOneShot(sounds[0]);
        }   
    }



    public bool PlayMusic=true;
    public void MusicChanger()
    {
        if (PlayMusic)
        {
            PlaySounds.Play();
        }
        else
        {
            PlaySounds.Stop();
        }
    }

    //PlayerPrefs.SetInt("MusicStatus", 0);
    

}
