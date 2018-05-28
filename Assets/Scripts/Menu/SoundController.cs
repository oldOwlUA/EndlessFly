using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController instance = null;
    public AudioSource _tapSound;
    public List<AudioSource> rightTapSounds = new List<AudioSource>();

    private int changeRightTap = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //на тапе на любой кнопке
    public void OnTap()
    {
        _tapSound.Stop();
        _tapSound.Play();

    }
    //при правильном нахождении кота
    public void RightTap()
    {
        
        rightTapSounds[changeRightTap].Stop();
        rightTapSounds[changeRightTap].Play();
        changeRightTap++;
        if(changeRightTap > rightTapSounds.Count - 1)
        {
            changeRightTap = 0;
        }
    }
}
