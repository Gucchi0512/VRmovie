using UnityEngine;
using System.Collections;

public class EyeControllerTarget : MonoBehaviour, Eyecontroller.IEyeControllerTarget
{
    GameObject maincamera;
    Color _color;

    void Awake() {

        // 最初はrigid同士が干渉しないようにする
        var rigid = gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = true;

    }

    public void OnEyeContollerHit(bool isOn) {
        Debug.Log("targetScriotsCAlled");
    }

    public void OnEyeContollerClick() {
        
    }
}