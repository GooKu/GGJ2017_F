using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	FadeController fadeEffect = null;

	Scene? lastScene = null;
	bool loading = false;

	static LevelManager singleton;

	public static LevelManager Singleton{
		get{
			return singleton;
		}
	}

	public int TotalLevels {
		get{
			return SceneManager.sceneCountInBuildSettings - 1;
		}
	}

	public int CurrentLevel {
		get;
		private set;
	}

	public void NextLevel(){
		this.StartCoroutine (this.ToLevel(this.CurrentLevel + 1));
	}

	public void PreviousLevel(){
		this.StartCoroutine (this.ToLevel(this.CurrentLevel - 1));
	}

	public void FirstLevel(){
		this.StartCoroutine (this.ToLevel(0, false));
	}

	void Awake(){
		if (singleton != null) {
			Debug.LogError ("Multiple LevelManager", this);
			return;
		}
		singleton = this;
	}
		
	IEnumerator ToLevel(int levelIndex, bool useFade = true){

		if (levelIndex < 0 || levelIndex >= this.TotalLevels) {
			yield break;
		} else if (this.loading) {
			yield break;
		}

		this.loading = true;

		if (this.fadeEffect != null && useFade) {
			yield return this.fadeEffect.FadeOut ();
		}

		// TODO: FADE IN FADE OUT
		if (this.lastScene != null){
			var u = this.lastScene.Value;
			if (u.isLoaded && u.IsValid()) {
				yield return SceneManager.UnloadSceneAsync (u);
			}
		}
			
		var count = SceneManager.sceneCount;
		yield return SceneManager.LoadSceneAsync(levelIndex+1, LoadSceneMode.Additive);
		var s = SceneManager.GetSceneAt (count);

		if (this.fadeEffect != null && useFade) {
			yield return this.fadeEffect.FadeIn ();
		}

		this.CurrentLevel = levelIndex;
		this.lastScene = s;
		this.loading = false;
	}
		
	void Start () {
		this.FirstLevel ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.N)) {
			NextLevel ();
		} else if (Input.GetKey(KeyCode.P)){
			PreviousLevel ();
		}
	}
}
