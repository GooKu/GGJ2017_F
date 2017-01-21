using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanCtrl : MonoBehaviour {

    public int active;
    public int mouse_down = 0;

    private float p = 800f;
    private Rigidbody2D rb;
    private Collider2D Coll;
    [SerializeField]
    private FanRotate fan_rotate;

    // Use this for initialization
    void Start () {		
	}
	
	// Update is called once per frame
	void Update ()
    {
        mouse_down = fan_rotate.mouse_down;
        if (active == 1 && mouse_down == 1)
        {
            rb = Coll.gameObject.GetComponent<Rigidbody2D>();
            //print(rb.velocity);
            rb.velocity = new Vector2(p, 100);
            //print(rb.velocity);
        }
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



}
