using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlaneCtrl : MonoBehaviour {

    private float speed = 100;
    private int dir = -1;
    private int Rotate = 1;
    private float r = 0;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //keep rotate
        if (Rotate == 1)
        {
            //if(t > 1)
            //{
            //    dir *= -1;
            //    t = 0;
            //}
            r = gameObject.transform.rotation.z;
            if (r > 0.5 || r < -0.5) { dir *= -1; }
            gameObject.transform.Rotate(new Vector3(0, 0, 1 * dir * speed * Time.deltaTime));
        }
        //t += Time.deltaTime;
    }
    void OnMouseDown()
    {
        if (Rotate == 0) Rotate = 1;
        else if (Rotate == 1) Rotate = 0;
    }
}
