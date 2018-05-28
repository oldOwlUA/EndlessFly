using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour {

    [SerializeField]
    internal Vector2 target;
    [SerializeField]
    private float rocketTurnSpeed;
    [SerializeField]
    private float rocketSpeed;
    [SerializeField]
    private float randomOffset;

    private float timerSinceLaunch_Contor;
    private float objectLifeTimerValue;


    void Start()
    {
       

        timerSinceLaunch_Contor = 0;
        objectLifeTimerValue = 10;      
    }
  
    void FixedUpdate()
    {
       
        timerSinceLaunch_Contor += Time.deltaTime;

        if (target != null)
        {
            if (GameObject.FindGameObjectWithTag("Finish"))
                target = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>().position;
            else target = new Vector2(0, -20);
            transform.position = Vector3.MoveTowards(transform.position, target, rocketSpeed * Time.deltaTime);
        }

        if (timerSinceLaunch_Contor > objectLifeTimerValue)
        {
            Destroy(transform.gameObject, 1);
        }
    }
}
