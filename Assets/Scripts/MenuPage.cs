using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour {

	public void StartGame(){
		LevelManager.Singleton.NextLevel ();
	}

    public void ShowCredits()
    {
        LevelManager.Singleton.ToCredits();
    }
}
