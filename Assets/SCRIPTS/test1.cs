using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(500f,0,0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
