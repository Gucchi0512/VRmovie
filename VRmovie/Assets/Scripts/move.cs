using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class move : MonoBehaviour {
    public float speed=1.0f;
    public Transform[] Startpos;
    [SerializeField]int count=0;
    Vector3 pos;
    VRCameraFade VRCameraFade;
    public Camera eyes;
    public bool flag=false;
    [SerializeField]bool fading;
    GameObject Sun;
    public SunRotate sunmanage;
    GameObject maincamera;
    public SeasonChange seasonmanage;
    Eyecontroller eyecontroller;
	// Use this for initialization
	void Start () {
        maincamera = GameObject.FindWithTag("MainCamera");
        Sun = GameObject.FindWithTag("Sun");
        sunmanage = Sun.GetComponent<SunRotate>();
        VRCameraFade = maincamera.GetComponent<VRCameraFade>();
        eyes = maincamera.GetComponent<Camera>();
        seasonmanage = GetComponent<SeasonChange>();
        eyecontroller = maincamera.GetComponent<Eyecontroller>();
        sunmanage.RotateofSun(0);
        pos = transform.position;
        FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
        if (!eyecontroller.enabled) {
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
    public void CameraMove() {
        GameObject cameraparent = maincamera.transform.parent.gameObject;
        cameraparent.transform.localPosition= new Vector3(5f, 3.8f, 11.1f);
        eyecontroller.enabled = !eyecontroller.enabled;
    }
    private IEnumerator FadeInCoroutine() {
        fading = true;
        yield return StartCoroutine(VRCameraFade.BeginFadeIn(true));
        Debug.Log("FadeIn Finished-2");
        fading = false;
        flag = false;
    }

    private IEnumerator FadeOutCoroutine() {
        fading = true;
        yield return StartCoroutine(VRCameraFade.BeginFadeOut(true));
        Debug.Log("FadeOut Finished-2");
        if (count == 0) CameraMove();
        this.transform.position = Startpos[count%4].position;
        sunmanage.RotateofSun(count % 4);
        seasonmanage.ChangeSeason(count % 4);
        count++;
        fading = false;
        flag = true;
    }
}
