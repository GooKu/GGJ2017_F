using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public AudioClip myAuioClip;

    private Vector2 initialPosition;
    private Vector2 offset;
    private Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        transform.Rotate(0, 0, 90);*/
        //transform.rotation +=Quaternion()
	}
    void OnMouseDown()
    {
        initialPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
        Debug.Log(initialPosition);
    }

    void OnMouseDrag()
    {
        Vector2 currentPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
        transform.position = new Vector3((currentPosition.x + initialPosition.x) / 2, (currentPosition.y + initialPosition.y) / 2, 0);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        transform.Rotate(0, 0, 90);
        //Debug.Log(cursorPosition);
    }

    void OnMouseUp()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip, 0.1f);
        //direction = direction / direction.magnitude;    // Normalize 
        gameObject.SetActive(false);
    }
}
