using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField]
    private float force;
    Rigidbody2D rb;

    private void Start()
    {
        force = Random.Range(3.0f, 6.0f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "magnet")
            print ("magnet");
    }
}
