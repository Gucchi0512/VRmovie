using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotate : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        
	}
	
	public void RotateofSun(int SeasonNum) {
        switch (SeasonNum) {
            case 0: { 
                this.transform.rotation = Quaternion.Euler(-240, 50, 90);
                break;
            }
            case 1: {
                this.transform.rotation = Quaternion.Euler(-155, 25, 110);
                break;
            }
            case 2: {
                this.transform.rotation = Quaternion.Euler(-175, 90, 240);
                break;
            }
            case 3: {
                this.transform.rotation = Quaternion.Euler(-160, 80, 260);
                break;
            }
            default: {
                break;
            }
        }
    }
}
