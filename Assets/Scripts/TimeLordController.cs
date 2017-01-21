using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLordController : MonoBehaviour {
	[SerializeField]
	GameObject morning;

	[SerializeField]
	GameObject noon;

	[SerializeField]
	GameObject night;


	void Awake(){
		TimeLordConfig.Changed += this.OnChanged;
		this.Invalidate ();
	}

	void Invalidate(){
		if (this.morning != null) {
			this.morning.SetActive (false);
		}

		if (this.noon != null) {
			this.noon.SetActive (false);
		}

		if (this.night != null) {
			this.night.SetActive (false);
		}

		var v = TimeLordConfig.Current;
		var target = (GameObject)null;

		switch (v){
		case TimeLordConfig.TimeSpan.Morning:
			target = this.morning;
			break;
		case TimeLordConfig.TimeSpan.Noon:
			target = this.noon;
			break;
		case TimeLordConfig.TimeSpan.Night:
			target = this.night;
			break;
		}

		if (target != null) {
			target.SetActive (true);
		}
	}
		
	void OnDestroy(){
		TimeLordConfig.Changed -= this.OnChanged;
	}

	void OnChanged (object sender, System.EventArgs e)
	{
		this.Invalidate ();
	}
}
