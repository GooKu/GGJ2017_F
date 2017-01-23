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
    Dictionary<string, object> levelData = new Dictionary<string, object>();

	public void ClearAll(){
	    this.ClearTrails ();
        this.levelData.Clear();

        instance = new GameDataManager ();
	}

    public void ClearLevel()
    {
        this.ClearTrails();
        this.levelData.Clear();

        // TODO:
    }

    public void SaveLevelTempData<T>(string key, T value)
    {
        if (!this.levelData.ContainsKey(key))
        {
            this.levelData.Add(key, value);
        }
        else
        {
            this.levelData[key] = value;
        }
    }

    public bool LoadLevelTempData<T>(string key, out T data)
    {
        if (!this.levelData.ContainsKey(key))
        {
            data = default(T);
            return false;
        }
        else
        {
            data = (T)this.levelData[key];
            return true;
        }
    }

    public int GetUnlockIndexChracter()
    {
		return chracterUnlockIndex;
    }

	public void ClearTrails(){
		foreach (var go in this.HistoryTrails.Where(v => v != null)) {
			GameObject.Destroy (go.gameObject);
		}
	}

	public List<TrailController> HistoryTrails{
		get{
			return this.historyTrails;
		}
	}
}
