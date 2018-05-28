using UnityEngine;
using System.Collections;

public class BoltMover : MonoBehaviour {

    // - public---------------------
    public float boltSpeed;
    public int shotPow;

    public float startScale = 0.1f;
    public float endScale  = 1;
    [Range(1,10)]
    public float ScaleFarctor = 1;
    // - private--------------------

    private Rigidbody2D BoltRB;

    

    // - methods--------------------
    void Start()
    {
        BoltRB = GetComponent<Rigidbody2D>();

        BoltRB.velocity = transform.up * boltSpeed;
        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    private void Update()
    {
        if (transform.localScale.x < endScale)
            transform.localScale += new Vector3( Time.deltaTime* ScaleFarctor, Time.deltaTime*ScaleFarctor, Time.deltaTime* ScaleFarctor);
    }


}
