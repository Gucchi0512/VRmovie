using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class move : MonoBehaviour {
    public float speed=1.0f;
    public Transform[] Startpos;
    int count=1;
    Vector3 pos;
    GameObject Eye;
    VRCameraFade VRCameraFade;
    public Camera eyes;
    public bool flag=false;
    bool fading;
    GameObject Sun;
    public SunRotate sunmanage;
    GameObject maincamera;
    SeasonChange seasonmanage;
	// Use this for initialization
	void Start () {
        maincamera = GameObject.FindWithTag("MainCamera");
        Sun = GameObject.FindWithTag("Sun");
        Eye = GameObject.FindWithTag("MainCamera");
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
        yield return StartCoroutine(VRCameraFade.BeginFadeIn(true));
        Debug.Log("FadeIn Finished");
        fading = false;
        flag = false;
    }

    private IEnumerator FadeOutCoroutine() {
        fading = true;
        yield return StartCoroutine(VRCameraFade.BeginFadeOut(true));
        Debug.Log("FadeOut Finished");
        this.transform.position = Startpos[count%4].position;
        sunmanage.RotateofSun(count % 4);
        seasonmanage.ChangeSeason(count % 4);
        count++;
        fading = false;
        flag = true;
    }
}
