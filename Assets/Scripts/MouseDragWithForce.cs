using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragWithForce : MonoBehaviour
{

    private Vector2 initialPosition;
    private Vector2 offset;
    private Vector2 direction;
    public float speed = 2.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void OnMouseDown()
    {
        initialPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y/Camera.main.pixelHeight);
        
        //Debug.Log(initialPosition);
    }

    void OnMouseDrag()
    {
        Vector2 currentPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
        direction = currentPosition - initialPosition;
        //Debug.Log(cursorPosition);
    }

    void OnMouseUp()
    {

        //direction = direction / direction.magnitude;    // Normalize 
        rb.velocity = -direction * speed * 1000 * Time.deltaTime;
        rb.isKinematic = false;

    }
}
