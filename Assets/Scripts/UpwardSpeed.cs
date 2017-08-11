using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), 2, Random.Range(-1.0f, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
