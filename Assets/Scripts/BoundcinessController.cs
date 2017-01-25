using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundcinessController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            return;
        }

        // TODO: 這會造成 BUG 先關閉，之後重新實做
        return;
       // var rigi2D = collider.GetComponent<Rigidbody2D>();
 

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
        // TODO: WHY? 不要亂設定
       // gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.0f;
    }
}
