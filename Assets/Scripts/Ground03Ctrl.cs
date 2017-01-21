using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground03Ctrl : MonoBehaviour {

    public AudioClip myAuioClip;

    private Rigidbody2D rb;
    private Collider2D c;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    void OnCollisionEnter2D(Collision2D Coll)
    {
        //gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.5f;
    }
    void OnCollisionExit2D(Collision2D Coll)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip, 0.1f);

        //rb = Coll.gameObject.GetComponent<Rigidbody2D>();
        //rb.velocity += new Vector2(0, -100);
    }
}
