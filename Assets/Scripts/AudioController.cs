using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CHe;


public class AudioController : SingletonObj<AudioController>
{
    public List<AudioSource> source;

    public bool FXOn;

    public void Set(int play)
    {
        if (play > 0)
            source[0].Play();
        else
            source[0].Stop();
    }


    public void SetFX(int playFX)
    {
        if (playFX > 0)
            OnFX();
        else
            OffFX();
    }
    public void OnFX()
    {       
        for (int i = 1; i < source.Count; i++)
        {
            source[i].volume = 1;
        }
    }
    public void OffFX()
    {
       
        for (int i = 1; i < source.Count; i++)
        {
            source[i].volume = 0;
        }
    }

}
