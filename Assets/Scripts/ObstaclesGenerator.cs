using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObstaclesGenerator : MonoBehaviour
{

    [Header("Временный  скрипт")]
    public List<GameObject> ObstPrefabs;

    AudioSource auS;

    public float delayToCreate;
    private float timer;

    void Start()
    {
        timer = delayToCreate;
        auS = gameObject.GetComponent<AudioSource>();
    }




    void FixedUpdate()
    {
        int ind = Random.Range(0, ObstPrefabs.Count);

        Vector3 randPos = gameObject.transform.localPosition;

        randPos.x = Random.Range(-3f, 3f);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = delayToCreate;
            Instantiate(ObstPrefabs[ind], randPos,Quaternion.identity,gameObject.transform);          
        }

    }
}
