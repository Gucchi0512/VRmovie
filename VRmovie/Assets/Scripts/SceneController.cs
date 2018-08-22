using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GameObject fadepanel;
    FadeController fade;
    GameObject eyes;
    Fade eye;
    public string scenename;// Use this for initialization
    void Start() {
        eyes = GameObject.FindWithTag("MainCamera");
        eye = eyes.GetComponent<Fade>();
        Debug.Log(eye);
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
        eye.FadeIn();
    }
}
