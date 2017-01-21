using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

    private Vector2 initialPosition;
    private Vector2 offset;
    private Vector2 direction;
    private Vector3 initialWorldPosition;

    public float speed = 200.0f;
    public GameObject arrow;

	public event System.EventHandler Fired;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

		if (arrow != null) {
			arrow.SetActive (false);
		}
    }

    void OnMouseDown()
    {
		if (!this.enabled)
        {
            initialPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
            initialWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            initialWorldPosition.z = 0;

			if (arrow != null) {
				arrow.transform.localScale = Vector3.zero;
				arrow.SetActive (true);
			}
            //Debug.Log(initialPosition);
        }
    }

    void OnMouseDrag()
    {

		if (!this.enabled) {
			Vector2 currentPosition = new Vector2 (Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
			direction = currentPosition - initialPosition;

			// Arrow control
			Vector3 currentWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			currentWorldPosition.z = 0;

			if (arrow != null) {
				arrow.transform.position = new Vector3 ((currentWorldPosition.x + initialWorldPosition.x) / 2, (currentWorldPosition.y + initialWorldPosition.y) / 2, 0);
				arrow.transform.rotation = Quaternion.LookRotation (Vector3.forward, currentWorldPosition - transform.position);
				arrow.transform.Rotate (0, 0, -90);
				arrow.transform.localScale = new Vector3 (Vector3.Distance (currentWorldPosition, initialWorldPosition) / 200, transform.localScale.y, transform.localScale.z);
			}
		}
    }

    void OnMouseUp()
    {
		if (!this.enabled)
        {
            direction = direction / direction.magnitude;    // Normalize 
            rb.velocity = -direction * speed * 1000 * Time.deltaTime;
            rb.isKinematic = false;

			if (arrow != null) {
				arrow.SetActive (false);
			}

			if (Fired != null) {
				Fired (this, System.EventArgs.Empty);
			}
        }
    }
}
