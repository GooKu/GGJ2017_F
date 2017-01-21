using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public event System.EventHandler Pass;

    void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
            if (Pass != null)
                Pass(this, System.EventArgs.Empty);

   //         if (LevelManager.Singleton != null) {
			//	LevelManager.Singleton.NextLevel ();
			//} else {
			//	Debug.Log ("必須從 Main 開始執行才能下一關");
			//}
		}
	}
}
