using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public enum Mode
    {
        PlayerControl,
        FollowTrager,
        Stop
    }

    private Transform traget;

    public void SetTraget(Transform traget)
    {
        this.traget = traget;
    }

    public void SetBoundary(Rect boundary)
    {

    }

    public void UpdateMode(Mode mode)
    {

    }
	
	void Update () {
		
	}
}
