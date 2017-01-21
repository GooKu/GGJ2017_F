using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

    private Vector2 initialPosition;
    private Vector2 offset;
    private Vector2 direction;
    public float speed = 200.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        initialPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Debug.Log(initialPosition);
    }

    void OnMouseDrag()
    {
        Vector2 currentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        direction = currentPosition - initialPosition;
        //Debug.Log(cursorPosition);
    }

    void OnMouseUp()
    {

        direction = direction / direction.magnitude;    // Normalize 
        rb.velocity = -direction * speed * Time.deltaTime;  
        
    }
}
