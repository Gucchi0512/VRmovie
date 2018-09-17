using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraColor : MonoBehaviour {
    Color color;
	// Use this for initialization
	void Start () {
        color = this.gameObject.GetComponent<Renderer>().material.color;
        //color = new Color()
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
