using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{

    VRCameraFade VRCameraFade;
    public string scenename;
    void Start() {
        VRCameraFade = FindObjectOfType<VRCameraFade>();
        FadeIn();
        Invoke("FadeOut", 7);
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
        SceneManager.LoadScene(scenename);
    }
}