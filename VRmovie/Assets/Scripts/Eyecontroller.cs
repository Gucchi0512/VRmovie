using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VRStandardAssets.Utils;

public class Eyecontroller : MonoBehaviour {
    RaycastHit hit;
    GameObject hitObject;
    public Image indicator;
    public Image mark;
    GameObject panel;
    FadeController fade;
    VRCameraFade VRCameraFade;
    Camera eyes;
    GameObject player;
    move eyemanage;
    SeasonChange seasonmanage;
    public bool hasclicked =false;
    public bool flag = true; //メニュー画面のみインジケータを出すためのフラグ
    public move.Delegate delegetemethod;
    BGMManager bgmManager;
    // Use this for initialization
    void Start () {
        bgmManager = GetComponentInParent<BGMManager>();
        delegetemethod = FadeIn;
        eyes = GetComponentInParent<Camera>();
        VRCameraFade = GetComponentInParent<VRCameraFade>();
        player = GameObject.FindWithTag("train");
        panel = GameObject.Find("Panel");
        eyemanage = player.GetComponent<move>();
        seasonmanage = player.GetComponent<SeasonChange>();
        FadeIn();
    }
	
	void AnimationIndicator(bool on) {
        if (on) {
            indicator.fillAmount += 0.5f * Time.deltaTime;
        } else {
            indicator.fillAmount = 0;
        }
    }

    private void FixedUpdate() {
        if (eyemanage.count > 4) {
            eyemanage.BackToTitle(delegetemethod);
            eyemanage.count = 1;
            eyemanage.sunmanage.RotateofSun(0);
        }
        // 物理オブジェクトのヒットテスト
        RaycastHit hit;
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
        if (hasHit) {
            //ターゲットが変更された場合
            if (hitObject != hit.collider.gameObject) {

            // 以前のターゲットを無効に
            if (hitObject) {
                 AnimationIndicator(false);
                 DispatchHitEvent(false);
            }

             //ヒットイベント発行
            hasclicked = false;
            hitObject = hit.collider.gameObject;
            DispatchHitEvent(true);
            } else {

                 //インジケーターアニメーション
                 if (hasclicked == false) {
                    AnimationIndicator(true);
                 }

                 if (indicator.fillAmount >= 1) {
                    hasclicked = true;
                    FadeOut();


                    indicator.fillAmount = 0;
                    DispatchClickEvent();
                 }
            }

        } else {
            //インジケーターアニメーション停止
            AnimationIndicator(false);
            DispatchHitEvent(false);
            hitObject = null;
            hasclicked = false;
        }     
    }
    public interface IEyeControllerTarget
    {
        void OnEyeContollerHit(bool isOn);
        void OnEyeContollerClick();
    }

    void DispatchHitEvent(bool isOn) {
        if (hitObject) {
            var target = hitObject.GetComponent<IEyeControllerTarget>();
            if (target != null) {
                target.OnEyeContollerHit(isOn);
            }
        }
    }

    void DispatchClickEvent() {
        if (hitObject) {
            var target = hitObject.GetComponent<IEyeControllerTarget>();
            if (target != null) {
                target.OnEyeContollerClick();
            }
        }
    }

    public void FadeIn() {
        StartCoroutine(FadeInCoroutine());
    }
    public void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine() {
        yield return StartCoroutine(VRCameraFade.BeginFadeIn(true));
        Debug.Log("FadeIn Finished");
    }

    private IEnumerator FadeOutCoroutine() {
        yield return StartCoroutine(VRCameraFade.BeginFadeOut(true));
        Debug.Log("FadeOut Finished");
        eyes.enabled = false;
        eyemanage.transform.position = eyemanage.Startpos[0].position;
        eyemanage.sunmanage.RotateofSun(0);
        seasonmanage.ChangeSeason(0);
        eyemanage.flag = true;
        eyemanage.eyes.enabled = true;
    }

}
