using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    GameObject fadepanel;
    FadeController fade;
    // Use this for initialization
    void Start() {
        fadepanel = GameObject.FindWithTag("panel");
        fade = fadepanel.GetComponent<FadeController>();
        Debug.Log(fadepanel);
        Debug.Log(fade);
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter(Collider other) {
        if (!fade.isFadeOut) {
            fade.isFadeOut = true;
        }

    }
}
