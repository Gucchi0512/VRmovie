using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonChange : MonoBehaviour {
    public GameObject[] Season = new GameObject[4];
    // Use this for initialization
    void Start() {
        foreach(GameObject s in Season) {
            s.gameObject.SetActive(false);
        }
    }

    public void ChangeSeason(int num) {
        Season[num].gameObject.SetActive(true);
        if(num!=0) Season[num - 1].gameObject.SetActive(false);
    }
}
