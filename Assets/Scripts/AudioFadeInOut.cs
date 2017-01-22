using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeInOut : MonoBehaviour {
    private AudioSource audio;
    private float time;

    public float threshold;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time < threshold)
        {
            audio.volume = Mathf.Lerp(0.0f, 0.8f, time / threshold);
        }
        if(!audio.isPlaying)
        {
            time = 0;
            audio.Play();
        }
	}
}
