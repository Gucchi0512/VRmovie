using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Eyecontroller : MonoBehaviour {
    RaycastHit hit;
    GameObject hitObject;
    Image indicator;
    GameObject panel;
    FadeController fade;
    public bool hasclicked =false;
	// Use this for initialization
	void Start () {
        indicator = GameObject.Find("Indicator").GetComponent<Image>();
        panel = GameObject.Find("Panel");
        fade = panel.GetComponent<FadeController>();
        SceneManager.activeSceneChanged += OnSceneChanged;
	}
	
	void AnimationIndicator(bool on) {
        if (on) {
            indicator.fillAmount += 0.8f * Time.deltaTime;
        } else {
            indicator.fillAmount = 0;
        }
    }

    private void FixedUpdate() {
        // 物理オブジェクトのヒットテスト
        RaycastHit hit;
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
        Debug.Log(hasHit);
        if (!fade.isFadeing) {
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

                    //インジケーターアニメーション開始
                    if (hasclicked == false) {
                        AnimationIndicator(true);
                    }

                    if (indicator.fillAmount >= 1) {
                        hasclicked = true;
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
    public void OnSceneChanged(Scene scene, Scene scene2) {
        indicator.gameObject.SetActive(false);
        GameObject.Find("Marker").gameObject.SetActive(false);
    }
}
