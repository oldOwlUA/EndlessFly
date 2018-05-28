using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBolt : MonoBehaviour {


    public float z;
    private float timerSinceLaunch_Contor;
    private float objectLifeTimerValue;

    // Use this for initialization
    void Start()
    {
        float scale = ScallerSc.Instance.defaultScele * 0.33f;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        timerSinceLaunch_Contor = 0;
        objectLifeTimerValue = 10;      
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       
        timerSinceLaunch_Contor += Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, z);
        transform.Translate(Vector2.up* Time.deltaTime*5);


        
        if (timerSinceLaunch_Contor > objectLifeTimerValue)
        {
           // Destroy(transform.gameObject, 1);
        }
    }
}
