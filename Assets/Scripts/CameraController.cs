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

    private float distance = 10;

    private Transform traget;
    private Rect boundary;
    private Mode currentMode;
    private float tragetZ;

    public void Init(Transform traget, Rect boundary)
    {
        this.traget = traget;
        this.boundary = boundary;
        tragetZ = traget.position.z - distance;
    }

    public void UpdateMode(Mode mode)
    {
        currentMode = mode;
    }
	
	void Update ()
    {
        switch (currentMode)
        {
            case Mode.FollowTrager:
                transform.position = Vector3.Lerp(transform.position, new Vector3(traget.position.x, traget.position.y, tragetZ), Time.deltaTime);
                break;
            case Mode.PlayerControl:
                break;
            case Mode.Stop:
                break;
        }

        if (transform.position.x > boundary.xMax)
            transform.position= new Vector3( boundary.xMax, transform.position.y, tragetZ) ;
        else if (transform.position.x < boundary.xMin)
            transform.position = new Vector3(boundary.xMin, transform.position.y, tragetZ);

        if (transform.position.y > boundary.yMax)
            transform.position = new Vector3(transform.position.x, boundary.yMax, tragetZ);
        else if (transform.position.x < boundary.xMin)
            transform.position = new Vector3(transform.position.x, boundary.yMin, tragetZ);

    }
}
