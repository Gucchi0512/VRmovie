using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {
    public AudioSource AudioSource;
    public AudioClip[] audioClips = new AudioClip[4];
	// Use this for initialization
	void Start () {
        
	}
	   
    public void BGM(int num) {
        AudioSource.clip = audioClips[num];
        AudioSource.Play();
    }
}
