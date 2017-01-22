using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public event System.EventHandler RunStarted;

	private bool run = false;

	public bool IsRunnig {
		get{
			return this.run;
		}
		private set{
			if (this.run != value) {
				this.run = value;

				if (value) {
					if (this.RunStarted != null) {
						this.RunStarted (this, System.EventArgs.Empty);
					}
				}
			}
		}
	}

	[Header("Core")]
	[SerializeField]
	private float timeLimit = 15;

	[Header("Components")]
	[SerializeField]
	CharacterController characterController;

	[SerializeField]
	CameraController cameraController;

	[SerializeField]
	BoxCollider2D cameraRegion;

    [SerializeField]
    private LevelUIManager levelUIManager;

    [SerializeField]
    private Door door;

	private bool showDie = true;

    void Reset(){
		this.characterController = this.transform.GetComponentInChildren<CharacterController> ();
		this.cameraController = this.transform.GetComponentInChildren<CameraController> ();
        this.cameraRegion = this.GetComponent<BoxCollider2D>();
        this.levelUIManager = this.GetComponentInChildren<LevelUIManager>();
        this.door = this.GetComponentInChildren<Door>();
    }

	void Start()
	{
		this.StartCoroutine (this.FlowCoroutine());
    }

	IEnumerator FlowCoroutine()
	{
		var infinity = this.timeLimit <= 0;

		// 初始化
		this.cameraController.UpdateMode (CameraController.Mode.Stop);
		this.cameraController.Init (this.characterController.transform, this.cameraRegion);

		this.cameraRegion.enabled = false;

		// 開啟選單
		var charList = this.characterController.UnLockCharacterInfoList;
		var charSelector = this.levelUIManager.CharacterSelector;
		charSelector.BeginSelect(charList);
		while (charSelector.IsSelecting) {
			yield return null;
		}

		var charId = charSelector.CharaterId;

		// 播放開始動畫
		yield return this.StartCoroutine(this.characterController.PlayStartAnim(charId));

		// 瀏覽模式
		this.cameraRegion.enabled = true;//gooku: tmp enable for get correct  bounds;
		{
			this.levelUIManager.SetCountDown (this.timeLimit, infinity);
			this.cameraController.UpdateMode (CameraController.Mode.PlayerControl);
			this.characterController.EnableCharacter (charId, this.cameraRegion);
		}
		this.cameraRegion.enabled = false;

		// 發射流程
		{
			var firing = false;
			var fired = false;
			var firingCancel = false;

			this.characterController.FiringCancel += (sender, e) => {firingCancel = true;};
			this.characterController.Firing += (sender, e) => {firing = true;};
			this.characterController.Fired += (sender, e) => {fired = true;};

			// 等待發射
			while (!fired) {
				yield return null;

				// 準備發射
				if (firing) {
					this.cameraController.UpdateMode (CameraController.Mode.Stop);
				}

				// 取消?
				if (firingCancel) {
					firing = false;
					firingCancel = false;
					this.cameraController.UpdateMode (CameraController.Mode.PlayerControl);
				}
			}
		}
			
		// 發射初始化
		var died = false;
		var passed = false;
		var giveup = false;

		this.characterController.Died += (sender, e) => {died = true;};
		this.door.Passed += (sender, e) => {passed = true;};

		this.cameraController.UpdateMode (CameraController.Mode.FollowTrager);
		this.cameraController.UpdateTarget (this.characterController.Current);

		this.IsRunnig = true;

		// 等遊戲結束
		var endTime = Time.time + this.timeLimit;
		while (!passed) {
			yield return null;

			if ((Time.time >= endTime && !infinity) || died || giveup) {
				// 時間限制到，演出失敗動畫 & 重置場景

				if (this.showDie) {
					yield return this.StartCoroutine (this.characterController.FailHandle ());
				}

				// Reset level
				this.KeepTrail ();
				this.ResetLevel ();
				yield break;
			}

			if (Input.GetKeyUp (KeyCode.R)) {
				giveup = true;
				this.showDie = false;
			}

			this.levelUIManager.SetCountDown(endTime - Time.time, infinity);
		}

		// 過關演出處理

		// 處理結束
		this.NextLevel();
	}

	void ResetLevel()
	{
		if (LevelManager.Singleton != null)
		{
			LevelManager.Singleton.ResetLevel();
		}
		else
		{
			Debug.Log("必須從 Main 開始執行才能下一關");
		}
	}

	void NextLevel(){
		if (LevelManager.Singleton != null)
		{
			LevelManager.Singleton.NextLevel();
		}
		else
		{
			Debug.Log("必須從 Main 開始執行才能下一關");
		}
	}

	void KeepTrail(){

		var history = GameDataManager.Instance.HistoryTrails;
			
		// Only keep one trail
		foreach (var go in history.Where(v => v != null)) {
			GameObject.Destroy (go.gameObject);
		}

		var trail = this.characterController.CurrentTrail;
		if (trail != null) {
			var go = trail.gameObject;
			go.transform.SetParent (null);
			GameObject.DontDestroyOnLoad (go);
			history.Add (trail);
		}
	}
}

public static class LevelControllerUtility{
	public static LevelController GetLevelController(this GameObject go){
		return go.transform.root.GetComponent<LevelController> ();
	}

	public static LevelController GetLevelController(this MonoBehaviour go){
		return go.transform.root.GetComponent<LevelController> ();
	}
}