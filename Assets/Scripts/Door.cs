using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public event System.EventHandler Passed;

	[SerializeField]
    AudioClip myAuioClip;

    void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
            if (this.Passed != null)
            {
                this.GetComponent<AudioSource>().PlayOneShot(this.myAuioClip, 0.1f);
                this.Passed(this, System.EventArgs.Empty);
            }
		}
	}
}
