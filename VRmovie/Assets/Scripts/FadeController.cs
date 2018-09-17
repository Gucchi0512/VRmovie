using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {//シーン遷移を含む
    float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理
    float interval = 2.0f;
    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
    public bool isFadeing = false;
    Image fadeImage;                //透明度を変更するパネルのイメージ
    GameObject eyes;
    Eyecontroller eye;
    public string scenename;
    void Start() {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
        SceneManager.sceneLoaded += OnSceneLoaded;
        //eyes = GameObject.Find("EyeCanvas");
        eye = this.gameObject.transform.parent.GetComponent<Eyecontroller>();
    }

    void Update() {
        if (eye.hasclicked) {
            
            isFadeOut = true;
        }
        if (isFadeIn) {
            isFadeing = true;
            StartFadeIn();
        }

        if (isFadeOut) {
            isFadeing = true;
            FedeOut();
            if (!isFadeOut) {
                SceneManager.LoadScene(scenename);
            }
        }
    }

    void StartFadeIn() {
        alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0) {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
            isFadeing = false;
        }

    }

    void FedeOut() {
        //float time=0.0f;
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;
        SetAlpha();
        
        if (alfa >= 1) {
            isFadeOut = false;   // d)完全に不透明になったら処理を抜ける
            isFadeing = false;

        }
        //SceneManager.LoadScene(scenename);
    }

    void SetAlpha() {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        /*isFadeIn = true;
        eye.flag = false;
        eye.indicator.enabled = false;
        eye.mark.enabled = false;
        */
    }
}
