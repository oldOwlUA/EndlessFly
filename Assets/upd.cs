using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(pos);
	}
}
