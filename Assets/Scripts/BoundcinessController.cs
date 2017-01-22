using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundcinessController : MonoBehaviour
{
    public event System.EventHandler OutOfBounds = null;

    private Bounds boundary;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (boundary == null)
        {
            Debug.Log("今天的我, 沒有極限^.<");
        }
        else
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ground01")
        {
            //pm.bounciness = 1.0f;
            //Debug.Log("Change bounciness");
            //gameObject.collider2D.
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.8f;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
        else if (collider.tag == "Ground02")
        {
            //pm.bounciness = 1.0f;
            //Debug.Log("Change bounciness");
            //gameObject.collider2D.
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.0f;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb.velocity.y < 0)
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * 1.5f);

        }
        else if (collider.tag == "Ground03")
        {
            //pm.bounciness = 1.0f;
            //Debug.Log("Change bounciness");
            //gameObject.collider2D.
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.6f;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void OnDestroy()
    {
        gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.0f;

    }

    public void SetBoundary(BoxCollider2D boundaryBoxCoilder)
    {
        boundary = boundaryBoxCoilder.bounds;
    }
}
