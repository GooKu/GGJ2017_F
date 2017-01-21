using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour {

	TrailRenderer trail;

	[SerializeField]
	Material[] materials;

	void Awake(){
		this.trail = this.GetComponent<TrailRenderer> (); 
		TimeLordConfig.Changed += this.OnChanged;
		this.Invalidate ();
	}

	void Invalidate()
	{
		var index = (int)TimeLordConfig.Current - 1;
		this.trail.sharedMaterial = materials[index];
	}

	void OnDestroy(){
		TimeLordConfig.Changed -= this.OnChanged;
	}

	void OnChanged (object sender, System.EventArgs e)
	{
		this.Invalidate ();
	}
}
