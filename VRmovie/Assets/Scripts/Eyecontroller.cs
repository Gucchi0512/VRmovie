using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Eyecontroller : MonoBehaviour {
    RaycastHit hitInfo;
    GameObject hitObject;
    Image indicator;
    GameObject panel;
    FadeController fade;
    public bool filled =false;
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
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hitInfo, 100);
        Debug.Log(hasHit);
        if (hasHit) {

            //ターゲットが変更された場合
            if (hitObject != hitInfo.collider.gameObject) {

                // 以前のターゲットを無効に
                if (hitObject) {
                    AnimationIndicator(false);
                }

                //ヒットイベント発行
                hitObject = hitInfo.collider.gameObject;

            } else {

                //インジケーターアニメーション開始
                if (filled == false) {
                    AnimationIndicator(true);
                }

                if (indicator.fillAmount >= 1) {
                    filled = true;
                    indicator.fillAmount = 0;
                }
            }

        } else {

            //インジケーターアニメーション停止
            AnimationIndicator(false);
            hitObject = null;
            filled = false;
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
    private void OnSceneChanged(Scene scene, Scene scene2) {
        indicator.gameObject.SetActive(false);
        GameObject.Find("Marker").gameObject.SetActive(false);
    }
}
