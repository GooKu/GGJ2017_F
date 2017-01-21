using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMachine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Vector2 player = collider.gameObject.transform.position;
            Vector2 position = transform.position;
            float distance = Vector2.Distance(position, player);
            Vector2 direction = (position - player).normalized;
            if (distance < 1)
                distance = 1;
            collider.GetComponent<Rigidbody2D>().AddForce(direction * 50000 / distance);
            
            
            /*float explosionForce = 1000.0f;
            Vector2 explosionPosition = (Vector2)transform.position;
            float explosionRadius = GetComponent<CircleCollider2D>().radius;

            ForceMode2D mode = ForceMode2D.Force;
           
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            var explosionDir = rb.position - explosionPosition;
            var explosionDistance = explosionDir.magnitude;

            // Normalize without computing magnitude again
            if (upwardsModifier == 0)
                explosionDir /= explosionDistance;
            else
            {
                // From Rigidbody.AddExplosionForce doc:
                // If you pass a non-zero value for the upwardsModifier parameter, the direction
                // will be modified by subtracting that value from the Y component of the centre point.
                explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
            //}

            rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    
*/
        }
    }
}
