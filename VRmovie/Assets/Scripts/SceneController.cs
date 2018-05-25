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
    
    public string scenename;// Use this for initialization
    void Start() {
        fadepanel = GameObject.FindWithTag("panel");
        fade = fadepanel.GetComponent<FadeController>();
        /*
        eyes = GameObject.Find("EyeCanvas");
        
        eye = this.GetComponent<Eyecontroller>();
          */
    }

    // Update is called once per frame
    void Update() {
        /*if (eye.hasclicked) {
            fade.isFadeOut = true;
            if (!fade.isFadeing) {
                SceneManager.LoadScene(scenename);
            }
        }*/
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!fade.isFadeOut) {
            fade.isFadeOut = true;
        }

    }
}
