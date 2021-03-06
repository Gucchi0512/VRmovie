﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class move : MonoBehaviour {
    public float speed=1.0f;
    public Transform[] Startpos;
    public int count=1;
    Vector3 pos;
    VRCameraFade VRCameraFade;
    public Camera eyes;
    public bool flag=false;
    bool fading;
    GameObject Sun;
    public SunRotate sunmanage;
    GameObject maincamera;
    SeasonChange seasonmanage;
    public delegate void Delegate();
    BGMManager bgmManager;
    // Use this for initialization
    void Start () {
        maincamera = GameObject.FindWithTag("MainCamera");
        Sun = GameObject.FindWithTag("Sun");
        bgmManager = maincamera.GetComponent<BGMManager>();
        sunmanage = Sun.GetComponent<SunRotate>();
        VRCameraFade = FindObjectOfType<VRCameraFade>();
        eyes = maincamera.GetComponent<Camera>();
        seasonmanage = GetComponent<SeasonChange>();
        sunmanage.RotateofSun(0);
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (eyes.enabled) {
            pos = transform.position;
            if (!fading && flag) {
                FadeIn();
            }
            if (!fading && !flag) {
                pos.z += speed;
                transform.position = pos;
            }
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
        bgmManager.BGM(count-1);
        yield return StartCoroutine(VRCameraFade.BeginFadeIn(true));
        Debug.Log("FadeIn Finished");
        fading = false;
        flag = false;
    }

    private IEnumerator FadeOutCoroutine() {
        if (!fading) {
            fading = true;
            yield return StartCoroutine(VRCameraFade.BeginFadeOut(true));
            Debug.Log("FadeOut Finished");
            if (count < 4) {
                this.transform.position = Startpos[count].position;
                sunmanage.RotateofSun(count);
                seasonmanage.ChangeSeason(count);

            }
            count++;
            fading = false;
            flag = true;
        }
    }

    public void BackToTitle(Delegate delegatemethod) {
        GameObject subCamera = GameObject.FindWithTag("SubCamera");
        Eyecontroller eyecontroller = subCamera.GetComponentInChildren<Eyecontroller>();
        Camera sub = subCamera.GetComponent<Camera>();
        sub.enabled = true;
        eyes.enabled = false;
        delegatemethod();
    }
}
