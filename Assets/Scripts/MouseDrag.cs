﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

    private Vector2 initialPosition;
    private Vector2 offset;
    private Vector2 direction;
    public float speed = 200.0f;
    public GameObject arrow;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void OnMouseDown()
    {
        initialPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
        //Debug.Log(initialPosition);
    }

    void OnMouseDrag()
    {
        Vector2 currentPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
        direction = currentPosition - initialPosition;

        // Arrow control
        transform.position = new Vector3((currentPosition.x + initialPosition.x) / 2, (currentPosition.y + initialPosition.y) / 2, 0);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        transform.Rotate(0, 0, 90);
        //Debug.Log(cursorPosition);
    }

    void OnMouseUp()
    {

        direction = direction / direction.magnitude;    // Normalize 
        rb.velocity = -direction * speed * 1000 * Time.deltaTime;
        rb.isKinematic = false;
        
    }
}
