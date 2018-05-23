using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GameObject fadepanel;
    FadeController fade;
    GameObject eyes;
    Eyecontroller eye;
    // Use this for initialization
    void Start() {
        fadepanel = GameObject.FindWithTag("panel");
        eyes = GameObject.Find("EyeCanvas");
        fade = fadepanel.GetComponent<FadeController>();
        eye = this.GetComponent<Eyecontroller>();
    }

    // Update is called once per frame
    void Update() {
        if (eye.hasclicked) {
            fade.isFadeOut = true;
            if (!fade.isFadeing) {
                SceneManager.LoadScene("Summer");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!fade.isFadeOut) {
            fade.isFadeOut = true;
        }

    }
}
