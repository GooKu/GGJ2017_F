using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;
#endif

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

    Scene? LastScene
    {
        get
        {
            return this.lastScene;
        }

        set
        {
            this.lastScene = value;
            if (value.HasValue)
            {
                SceneManager.SetActiveScene(value.Value);
            }
        }
    }

	public void NextLevel(){
        GameDataManager.Instance.ClearLevel();
        this.StartCoroutine (this.ToLevel(this.CurrentLevel + 1));
	}

	public void PreviousLevel(){
        GameDataManager.Instance.ClearLevel();
        this.StartCoroutine (this.ToLevel(this.CurrentLevel - 1));
	}

	public void FirstLevel(){
		GameDataManager.Instance.ClearAll ();
		this.StartCoroutine (this.ToLevel(0, false));
	}

	public void ResetLevel(){
		this.StartCoroutine (this.ToLevel(this.CurrentLevel));
	}

    public void ToCredits()
    {

        this.StartCoroutine(this.ToLevel(this.TotalLevels - 1));
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

        // Fade..
        if (this.fadeEffect != null && useFade) {
			yield return this.fadeEffect.FadeOut ();
		}

		if (this.LastScene != null){
			var u = this.LastScene.Value;
			if (u.isLoaded && u.IsValid()) {
				yield return SceneManager.UnloadSceneAsync (u);
			}
		}

        this.CurrentLevel = levelIndex;

        var count = SceneManager.sceneCount;
		yield return SceneManager.LoadSceneAsync(levelIndex+1, LoadSceneMode.Additive);
		var s = SceneManager.GetSceneAt (count);

		if (this.fadeEffect != null && useFade) {
			yield return this.fadeEffect.FadeIn ();
		}

#if UNITY_EDITOR
        Debug.Log("Level: " + this.CurrentLevel.ToString() + "/ " + this.TotalLevels.ToString());
#endif

        this.LastScene = s;
        this.loading = false;
	}
		
	void Start () {
		#if UNITY_EDITOR
		if (SceneManager.sceneCount > 1){
			// 編輯模式
			var targetScene = new Scene();
			var buildIndex = -1;
			var buildScenes = EditorBuildSettings.scenes;
			for (var i = 0; i < SceneManager.sceneCount; i++){
				var s = SceneManager.GetSceneAt(i);
				if (s.buildIndex != 0){
					buildIndex = s.buildIndex;
					targetScene = s;
					break;
				}
			}

			if (buildIndex == -1){
				Debug.LogWarning("場景未加入到 BuildSettings, 無法正常啟用 LevelManager");
			}
			else
			{
				this.CurrentLevel = buildIndex - 1;
				this.LastScene = targetScene;
                return;
			}
		}
		#endif

		this.FirstLevel ();
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.N)) {
			this.NextLevel ();
		} else if (Input.GetKeyUp (KeyCode.P)) {
			this.PreviousLevel ();
		} 

		if (Input.GetKeyUp (KeyCode.T)) {
			var v = (int)TimeLordConfig.Debug + 1;
			var vs = Mathf.Repeat ((float)v, 4);
			TimeLordConfig.Debug = (TimeLordConfig.TimeSpan)((int)vs);
		}
	}
}
