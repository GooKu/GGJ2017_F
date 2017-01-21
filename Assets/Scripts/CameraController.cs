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

    [SerializeField]
    private Transform traget;
    [SerializeField]
    private BoxCollider2D boundaryBoxCoilder;
    [SerializeField]
    private Mode currentMode;
    private float tragetZ = -250;
    private Vector3 tragetPostion;
    private Vector2 currentPos;
    private Vector2 lastPos;
    private Vector3 deltaPos { get { return currentPos - lastPos; } }
    private float screenRate;

    public void Init(Transform traget, BoxCollider2D boundaryBoxCoilder)
    {
        this.traget = traget;
        this.boundaryBoxCoilder = boundaryBoxCoilder; ;
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

        if (transform.position.x > boundaryBoxCoilder.bounds.max.x)
            transform.position= new Vector3( boundaryBoxCoilder.bounds.max.x, transform.position.y, tragetZ) ;
        else if (transform.position.x < boundaryBoxCoilder.bounds.min.x)
            transform.position = new Vector3(boundaryBoxCoilder.bounds.min.x, transform.position.y, tragetZ);

        if (transform.position.y > boundaryBoxCoilder.bounds.max.y)
            transform.position = new Vector3(transform.position.x, boundaryBoxCoilder.bounds.max.y, tragetZ);
        else if (transform.position.y < boundaryBoxCoilder.bounds.min.y)
            transform.position = new Vector3(transform.position.x, boundaryBoxCoilder.bounds.min.y, tragetZ);
    }
}
