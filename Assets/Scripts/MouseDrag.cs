using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

    private Vector2 initialPosition;
    private Vector2 direction;
    private Vector3 initialWorldPosition;
    private GameObject arrow;

    public float speed = 50.0f;
    public GameObject arrowPrefab;
    public bool isDragingWithForce;

	public event System.EventHandler Fired;
    public event System.EventHandler Firing;
    public event System.EventHandler FiringCancel;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        direction = Vector3.zero;
    }

    void OnMouseDown()
    {
		if (this.enabled)
        {
            arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            initialPosition = new Vector2(Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
            initialWorldPosition = transform.position;
            initialWorldPosition.z = 0;

			if (arrow != null) {
				arrow.transform.localScale = Vector3.zero;
				arrow.SetActive (true);
			}
            if(this.Firing != null)
            {
                this.Firing(this, System.EventArgs.Empty);
            }
            //Debug.Log(initialPosition);
        }
    }

    void OnMouseDrag()
    {

		if (this.enabled) {
			Vector2 currentPosition = new Vector2 (Input.mousePosition.x / Camera.main.pixelWidth, Input.mousePosition.y / Camera.main.pixelHeight);
			direction = currentPosition - initialPosition;

			// Arrow control
			Vector3 currentWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			currentWorldPosition.z = 0;

			if (arrow != null) {
                Vector2 arrowPosition = new Vector3(initialWorldPosition.x + (initialWorldPosition.x - currentWorldPosition.x) / 2, initialWorldPosition.y + (initialWorldPosition.y - currentWorldPosition.y) / 2);
                arrow.transform.position = new Vector3(arrowPosition.x, arrowPosition.y, 0);
                arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, currentWorldPosition - transform.position);
				arrow.transform.Rotate (0, 0, -90);
				arrow.transform.localScale = new Vector3 (Vector3.Distance (currentWorldPosition, initialWorldPosition) / 200, transform.localScale.y, transform.localScale.z);
			}
		}
    }

    void OnMouseUp()
    {
		if (this.enabled)
        {
            if (direction.magnitude > 0.1)
            {
                if (!isDragingWithForce)
                {
                    direction = direction.normalized;
                    rb.velocity = -direction * speed * 1000 * Time.deltaTime;
                }
                else
                {
                    rb.velocity = -direction * speed * 4000 * Time.deltaTime;
                }
                rb.isKinematic = false;

                if (this.Fired != null)
                {
                    this.Fired(this, System.EventArgs.Empty);
                }
            }
            else
            {
                if (this.FiringCancel != null)
                {
                    this.FiringCancel(this, System.EventArgs.Empty);
                }
            }
            if (arrow != null)
            {
                Destroy(arrow);

            }
        }
    }
}
