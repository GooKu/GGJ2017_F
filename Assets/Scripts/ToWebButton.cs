using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToWebButton : AnimButton {
	[SerializeField]
	string url;

	protected override void Do ()
	{
#if UNITY_WEBGL
        Application.ExternalCall("openLink", this.url);
#else
        Application.OpenURL (this.url);
#endif
    }
}
