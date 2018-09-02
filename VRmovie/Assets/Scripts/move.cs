using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class move : MonoBehaviour {
    public float speed=1.0f;
    public Transform[] Startpos;
    int count=0;
    Vector3 pos;
    GameObject Eye;
    VRCameraFade VRCameraFade;
    bool flag=false;
    bool fading;
	// Use this for initialization
	void Start () {
        Eye = GameObject.FindWithTag("MainCamera");
        VRCameraFade = FindObjectOfType<VRCameraFade>();
        pos = transform.position;
        FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
        pos=transform.position;
        if (!fading && flag) {
             FadeIn();
        }
        if (!fading && !flag) {
            pos.z += speed;
            transform.position = pos;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Endpoint") {
            FadeOut();
        }
    }

    public void FadeIn() {
        StartCoroutine(FadeInCoroutine());
    }
    public void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine() {
        fading = true;
        yield return StartCoroutine(VRCameraFade.BeginFadeIn(true));
        Debug.Log("FadeIn Finished");
        fading = false;
        flag = false;
    }

    private IEnumerator FadeOutCoroutine() {
        fading = true;
        yield return StartCoroutine(VRCameraFade.BeginFadeOut(true));
        Debug.Log("FadeOut Finished");
        this.transform.position = Startpos[count].position;
        count++;
        fading = false;
        flag = true;
    }
}
