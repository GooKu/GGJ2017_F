using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGround01 : MonoBehaviour {

    private int move = 1;
    private float hight;
    private int dir = -1;
    private float speed = 180;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (move == 1)
        {
            hight = transform.position.y;
            if (hight > 150 || hight < -150) { dir *= -1; }
            transform.Translate(new Vector2(0, 1 * dir * speed * Time.deltaTime));
        }

    }
    void OnMouseDown()
    {
        if (move == 0) move = 1;
        else if (move == 1) move = 0;
    }
}
