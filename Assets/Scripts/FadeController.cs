using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

	[SerializeField]
	float fadeIn = .4f;

	[SerializeField]
	float fadeOut = .4f;
	
	[SerializeField]
	GameObject root;

	[SerializeField]
	Image image;

	Material material;

	public IEnumerator FadeIn(){
		return this.Fade (1, 0, this.fadeIn, true);
	}

	public IEnumerator FadeOut(){
		return this.Fade (0, 1, this.fadeOut, false);
	}

	void Start(){
		this.root.SetActive (false);

		material = new Material (this.image.material);
		this.image.material = material;
	}

	IEnumerator Fade(float begin, float end, float duration, bool off){
		this.root.SetActive (true);

		var tBegin = Time.realtimeSinceStartup;
		var tEnd = tBegin + duration;

		while (Time.realtimeSinceStartup < tEnd){
			var t = Mathf.InverseLerp(tBegin, tEnd, Time.realtimeSinceStartup);
			var v = Mathf.Lerp(begin, end, t);

			var c = material.color;
			c.a = v;

			material.color = c;

			yield return null;
		}

		if (off) {
			this.root.SetActive (false);
		}
	}
}
