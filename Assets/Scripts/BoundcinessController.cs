using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundcinessController : MonoBehaviour
{
    public PhysicsMaterial2D pm;
	// Use this for initialization
	void Start () {
        //pm = (PhysicsMaterial2D)Resources.Load("Physics/Character02");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Ground01")
        {
            //pm.bounciness = 1.0f;
            Debug.Log("Change bounciness");
            //gameObject.collider2D.
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.8f;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void OnDestroy()
    {
        gameObject.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.0f;
            
    }
}
