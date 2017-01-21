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
    private Bounds boundary;

    private float camerOffsetX = 300;
    private float cameraSpeed = 2;

    private void Awake()
    {
        if (boundaryBoxCoilder != null)
        {
            updateBoundary(boundaryBoxCoilder);
            screenRate = (float)Screen.width / Screen.height;
        }
    }

    public void Init(Transform traget, BoxCollider2D boundaryBoxCoilder)
    {
        this.traget = traget;
        updateBoundary (boundaryBoxCoilder); ;
        tragetZ = traget.position.z - distance;
        screenRate = (float)Screen.width / Screen.height;
    }

    public void UpdateMode(Mode mode)
    {
        currentMode = mode;
    }

	public void UpdateTarget(Transform t){
		this.traget = t;
	}

    private void updateBoundary(BoxCollider2D boundaryBoxCoilder)
    {
        float orthographicSize = GetComponent<Camera>().orthographicSize*2;
        boundary = new Bounds(boundaryBoxCoilder.bounds.center, 
        new Vector3(boundaryBoxCoilder.size.x - (orthographicSize * Screen.width / Screen.height), boundaryBoxCoilder.size.y - orthographicSize, 0));
    }
	
	void Update ()
    {
        switch (currentMode)
        {
            case Mode.FollowTrager:
                tragetPostion = new Vector3(traget.position.x+ camerOffsetX, traget.position.y, tragetZ);
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
                    tragetPostion = new Vector3((deltaPos.x / Screen.width)* screenRate, deltaPos.y / Screen.height, 0) * Camera.main.orthographicSize * 16;
                    tragetPostion = transform.position - tragetPostion;
                    lastPos = currentPos;
                }

//                Debug.Log(transform.position+", "+tragetPostion);

                break;
            case Mode.Stop:
                return ;
        }

        transform.position = Vector3.Lerp(transform.position, tragetPostion, Time.deltaTime* cameraSpeed);

        if (transform.position.x > boundary.max.x)
            transform.position= new Vector3(boundary.max.x, transform.position.y, tragetZ) ;
        else if (transform.position.x < boundary.min.x)
            transform.position = new Vector3(boundary.min.x, transform.position.y, tragetZ);

        if (transform.position.y > boundary.max.y)
            transform.position = new Vector3(transform.position.x, boundary.max.y, tragetZ);
        else if (transform.position.y < boundary.min.y)
            transform.position = new Vector3(transform.position.x, boundary.min.y, tragetZ);
    }
}
