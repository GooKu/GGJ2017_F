using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
	static GameDataManager instance;

	public static GameDataManager Instance
	{
		get{
			if (instance == null) {
				instance = new GameDataManager ();
			}
			return instance;
		}
	}

	int chracterUnlockIndex = 2;//gooku: tmp fot test (INIT VALUE)
	List<TrailController> historyTrails = new List<TrailController>();

	public void Reset(){
		if (instance != null) {
			var history = this.historyTrails;
			foreach (var go in history.Where(v => v != null)) {
				GameObject.Destroy (go.gameObject);
			}
		}

		instance = new GameDataManager ();
	}

    public int GetUnlockIndexChracter()
    {
		return chracterUnlockIndex;
    }

	public List<TrailController> HistoryTrails{
		get{
			return this.historyTrails;
		}
	}
}
