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

    private float distance = 250;

    private Transform traget;
    private Rect boundary;
    private Mode currentMode;
    private float tragetZ;
    private Vector3 tragetPostion;
    public Vector2 currentPos;
    public Vector2 lastPos;
    public Vector3 deltaPos { get { return currentPos - lastPos; } }
    private float screenRate;

    public void Init(Transform traget, Rect boundary)
    {
        this.traget = traget;
        this.boundary = boundary;
        tragetZ = traget.position.z - distance;
        screenRate = (float)Screen.width / Screen.height;
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
                tragetPostion = new Vector3(traget.position.x, traget.position.y, tragetZ);
                break;
            case Mode.PlayerControl:
                if (Input.GetMouseButtonDown(0))
                {
                    currentPos = lastPos = Input.mousePosition;
                    return;
                }

                tragetPostion = transform.position;

                if (Input.GetMouseButton(0))
                {
                    currentPos = Input.mousePosition;
                    tragetPostion = new Vector3(deltaPos.x / Screen.width  * screenRate, deltaPos.y / Screen.height , 0)*Camera.main.orthographicSize*16;
                    tragetPostion = transform.position - tragetPostion;
                    lastPos = currentPos;
                }

                break;
            case Mode.Stop:
                return ;
        }

        transform.position = Vector3.Lerp(transform.position, tragetPostion, Time.deltaTime);

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
