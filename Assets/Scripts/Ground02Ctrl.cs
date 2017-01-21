using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground02Ctrl : MonoBehaviour {

    public AudioClip myAuioClip;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionExit2D(Collision2D Coll)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip, 0.1f);
        rb = Coll.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity += new Vector2(0, 200);
    }
}
