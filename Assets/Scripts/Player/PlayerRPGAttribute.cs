using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerRPGAttribute
{
    public float TimeShild = 0;
    public float TimeMagnet =0;
   // public float PowerUpShot =0;

    
    public int ProbabMagnet = 0;
    public int ProbabPowerUp = 0;

    public PlayerRPGAttribute()
    {
        TimeShild = 0;
        TimeMagnet = 0;
      //  PowerUpShot = 0;
        
        ProbabMagnet = 0;
        ProbabPowerUp = 0;
    }

}
