using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMenuButton : AnimButton {
	protected override void Do ()
	{
		LevelManager.Singleton.FirstLevel ();
	}
}
