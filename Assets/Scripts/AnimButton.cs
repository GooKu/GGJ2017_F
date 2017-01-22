using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimButton : MonoBehaviour {

	[SerializeField]
	Animator animator;

	void Reset(){
		this.animator = this.GetComponent<Animator> ();
	}

	void OnMouseEnter () {
		this.animator.SetBool ("Hover", true);
	}

	void OnMouseExit () {
		this.animator.SetBool ("Hover", false);
	}

	void OnMouseUp(){
		Do();
	}	

	protected abstract void Do ();
}
