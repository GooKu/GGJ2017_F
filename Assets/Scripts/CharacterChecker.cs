using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChecker : MonoBehaviour
{
    public event System.EventHandler Still;
    public event System.EventHandler OutOfBounds ;

    private static readonly float stillCheckTime = 1;
    private float currentCheckTime;
    private bool isStillCheck = false;
    private static readonly Vector2 stillCheckVect = new Vector2(60f, 60f);
    private Rigidbody2D rigidbody;
    private Bounds boundary;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isStillCheck)
            stillCheck();

        if (boundary == null)
        {
            Debug.Log("今天的我, 沒有極限^.<");
        }
        else
            boundaryCheck();
    }

    private void boundaryCheck()
    {
        if (transform.position.x > boundary.max.x ||
            transform.position.x < boundary.min.x ||
            transform.position.y > boundary.max.y ||  
            transform.position.y < boundary.min.y)
        {
            if (OutOfBounds != null)
                OutOfBounds(this, System.EventArgs.Empty);
                OutOfBounds = null;
        }
    }

    private void stillCheck()
    {
        if (rigidbody.velocity.magnitude != Mathf.Min(rigidbody.velocity.magnitude, stillCheckVect.magnitude))
        {
            currentCheckTime = 0;
            return;
        }

        currentCheckTime += Time.deltaTime;

        if (currentCheckTime < stillCheckTime)
            return;

        isStillCheck = false;
        if (Still != null)
            Still(this, System.EventArgs.Empty);
    }

    public void EnableStilCheck( System.EventHandler Still)
    {
        isStillCheck = true;
        currentCheckTime = 0;
        this.Still += Still;
    }

    public void SetBoundary(BoxCollider2D boundaryBoxCoilder)
    {
        boundary = boundaryBoxCoilder.bounds;
    }
}
