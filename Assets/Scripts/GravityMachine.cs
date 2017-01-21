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
            float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
            Vector3 direction = Vector3.Normalize(transform.position - collider.gameObject.transform.position);

            //collider.rigidbody2D.F
        }
    }
}
