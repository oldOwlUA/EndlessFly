using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenControll : MonoBehaviour
{


    public Text TimeMagnet;
    public Text TimeShild;
    public Text ShotPower;
    public Text ProbCoin;
    public Text ProbMagnet;
    public Text ProbPA;
    
    public GameObject g;

    void Update()
    {
        int i = g.GetComponent<SnapScroling>().selectedPanId;

        TimeMagnet.text =  GameController.Instance.Heroes[i].attribute.TimeMagnet.ToString();
        TimeShild.text = GameController.Instance.Heroes[i].attribute.TimeShild.ToString();

        var shot = GameController.Instance.BasicShot * (GameController.Instance.Heroes[i].Level + 1); ;
        
        ShotPower.text = shot.ToString();

        ProbMagnet.text = GameController.Instance.Heroes[i].attribute.ProbabMagnet.ToString();
        ProbPA.text =      GameController.Instance.Heroes[i].attribute.ProbabPowerUp.ToString();

        int s =  100 - GameController.Instance.Heroes[i].attribute.ProbabMagnet - GameController.Instance.Heroes[i].attribute.ProbabPowerUp;

        ProbCoin.text = s.ToString();        
    }
}