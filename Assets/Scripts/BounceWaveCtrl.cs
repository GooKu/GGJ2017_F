using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWaveCtrl : MonoBehaviour {

    private Vector2 ball_pos;
    private Vector2 fan_pos;
    private Vector2 Force;
    public int active;
    private Rigidbody2D rb;
    private Collider2D Coll;
    public AudioClip myAuioClip;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Coll = c;
        active = 1;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        active = 0;
    }
    void OnMouseDown()
    {
        if (active == 1)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip,0.1f);
            ball_pos = Coll.gameObject.transform.position;
            fan_pos = gameObject.transform.position;
            Force = (fan_pos - ball_pos);
            Force.Normalize();
            Force = Force * -800;

            rb = Coll.gameObject.GetComponent<Rigidbody2D>();
            //print(rb.velocity);
            rb.velocity = Force;
            //print(rb.velocity);
        }

    }
}
