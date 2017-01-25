using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSoundFX : MonoBehaviour {
    public AudioClip myAuioClip;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D Coll)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip);
    }
    void OnCollisionEnter2D(Collision2D Coll)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip);
    }
}
