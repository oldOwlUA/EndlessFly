using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    public GameObject Magnet;
    public GameObject ParticEffects;
    public float durationMagnet;

    public bool iswork = false;


    [SerializeField]
    private float multipil = 5;
    float scl;
    float scaleP;
    [SerializeField]

    private void Start()
    {
        durationMagnet = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].attribute.TimeMagnet;

        scl = (Camera.main.orthographicSize * Camera.main.aspect);
         scaleP = (scl * 2) / 5;
    }

    private float time;

    public void resetTime()
    {
        time = durationMagnet;
    }
    public void resetMagnet()

    {
        time = durationMagnet;
    }

    public void OffMagnet()
    {
        iswork = false;
        ParticEffects.transform.localScale = new Vector3(0.3f* scaleP, 0.3f * scaleP, 0.3f * scaleP);
        ParticEffects.SetActive(false);
        Magnet.SetActive(false);
    }

    public  IEnumerator MagnetCur()
    {
        float tempTime = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].attribute.TimeMagnet;
        if (tempTime <= 0)
            time = durationMagnet = 10;
        else time = durationMagnet = tempTime;

        iswork = true;
        ParticEffects.SetActive(true);
        bool t = true;
        

        print("one");


        while (ParticEffects.transform.localScale.x < (3.7 * scaleP))
        {
            ParticEffects.transform.localScale += new Vector3(Time.deltaTime*multipil, Time.deltaTime * multipil, Time.deltaTime * multipil);
            yield return null;
        }
        print("two");


        Magnet.SetActive(true);
        while (t)
        {
            if (time > 0)
                time -= Time.deltaTime;
            else t = false;

            yield return null;
        }
        print("tree");
        Magnet.SetActive(false);
        iswork = false;


        while (ParticEffects.transform.localScale.x > (0.3 * scaleP))
        {
            if (time == durationMagnet)
            {
                StartCoroutine(MagnetCur());                
            }
            ParticEffects.transform.localScale -= new Vector3(Time.deltaTime * multipil, Time.deltaTime* multipil, Time.deltaTime * multipil);
            yield return null;
        }
        print("four");        
        ParticEffects.SetActive(false);
        resetTime();
    }
}