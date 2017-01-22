using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToWebButton : AnimButton {
	[SerializeField]
	string url;

	protected override void Do ()
	{
		Application.OpenURL (this.url);
	}
}
