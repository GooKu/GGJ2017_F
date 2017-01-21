using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public Transform Traget;
    public CameraController controller;

	// Use this for initialization
	void Start () {
        controller.Init(Traget, new Rect(0, 0,200, 200));
        controller.UpdateMode(CameraController.Mode.FollowTrager);
//        Debug.Log(Camera.main.fieldOfView);
        //controller.UpdateMode(CameraController.Mode.PlayerControl);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
