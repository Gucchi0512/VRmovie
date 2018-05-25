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
    public Image indicator;
    public Image mark;
    GameObject panel;
    FadeController fade;
    string scenename;
    public bool hasclicked =false;
    public bool flag = true; //メニュー画面のみインジケータを出すためのフラグ
	// Use this for initialization
	void Start () {
        indicator = GameObject.Find("Indicator").GetComponent<Image>();
        mark = GameObject.Find("Marker").GetComponent<Image>();
        panel = GameObject.Find("Panel");
        fade = panel.GetComponent<FadeController>();
        SceneManager.activeSceneChanged += OnSceneChanged;
        scenename = SceneManager.GetActiveScene().name;
    }
	
	void AnimationIndicator(bool on) {
        if (on) {
            indicator.fillAmount += 0.5f * Time.deltaTime;
        } else {
            indicator.fillAmount = 0;
        }
    }

    private void FixedUpdate() {
        // 物理オブジェクトのヒットテスト
        RaycastHit hit;
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
        if (scenename!="Title") {
            hasHit = false;
            indicator.gameObject.SetActive(false);
            mark.gameObject.SetActive(false);
        }
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
        
    }
}
