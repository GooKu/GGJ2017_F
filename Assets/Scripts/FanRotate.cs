using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour {
    
    public int mouse_down;
    public AudioClip myAuioClip;

    private int speed = 0;
    private float t = 0;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, -1 * speed * Time.deltaTime));
        if (mouse_down == 1)
        {            
            speed = 1000;
        }
        else if (mouse_down == 0)
        {
            if (speed > 0)
            {
                speed -= 20;
                if (speed < 0) speed = 0;
            }
        }
    }
    void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip,0.02f);
        mouse_down = 1;
        print(mouse_down);
    }
    void OnMouseUp()
    {
        mouse_down = 0;
    }

}
