using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBoltControll : MonoBehaviour
{

    public float boltCount;

    [SerializeField]
    int minBoltCount;
    [SerializeField]
    int maxBoltCount;
    public GameObject boltPref;
    public float DestroyTime = 1;
    [SerializeField]
    float angle;
    [SerializeField]
    float defAngle;

    void Start()
    {
        boltCount = Random.Range(minBoltCount, maxBoltCount);
        float angleStep = angle / (boltCount - 1);
        defAngle += angle / 2; 
            //print(angleStep);

        for (int i = 0; i < boltCount; i++)
        {
            GameObject gm = Instantiate(boltPref, transform.position, Quaternion.identity, GameController.Instance.InstRootObjects[0]);
            if (i == 0)
                gm.GetComponent<PointerBolt>().z = (defAngle);

            else gm.GetComponent<PointerBolt>().z = (defAngle) - angleStep * i;
        }
        StartCoroutine(Autodestroy());
    }

    IEnumerator Autodestroy()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }
}
