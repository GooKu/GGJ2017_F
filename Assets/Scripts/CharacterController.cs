using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event System.EventHandler Fired;

	public Transform Current {
		get;
		private set;
	}

	void Awake(){
		// TODO: 僅註冊有使用到的物件
		var mds = GetComponentsInChildren<MouseDrag> ();
		foreach (var md in mds) {
			md.Fired += this.OnFired;
		}
	}

	void OnFired (object sender, System.EventArgs e)
	{
		// TODO: 設定成使用到的物件
		this.Current = (sender as MouseDrag).transform;

		if (this.Fired != null) {
			this.Fired (this, e);
		}
	}
}
