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

		var died = false;
		var re = false;

        var doSelect = false;

		var firing = false;
		var fired = false;
		var firingCancel = false;

		this.characterController.FiringCancel += (sender, e) => {firingCancel = true;};
		this.characterController.Firing += (sender, e) => {firing = true;};
		this.characterController.Fired += (sender, e) => {fired = true;};
		this.characterController.Died += (sender, e) => { died = true; };

		this.levelUIManager.ReturnButtonClicked += (sender, e) => { re = true; };
        this.levelUIManager.SelectionButtonClicked += (sender, e) => { doSelect = true; };

		// 初始化
		this.cameraController.UpdateMode (CameraController.Mode.Stop);
		this.cameraController.Init (this.characterController.transform, this.cameraRegion);
		this.cameraRegion.enabled = false;
        this.levelUIManager.Mode = LevelUIManager.UIMode.None;

        var charId = GameDataManager.Instance.CharacterId;

        // 播放開始動畫
        yield return this.StartCoroutine(this.characterController.PlayStartAnim(charId));

        SELECT_CHAR:

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
            fired = false;
			firing = false;
			firingCancel = false;

            this.levelUIManager.Mode = LevelUIManager.UIMode.WaitFiring;

            // 等待發射
            while (!fired) {
				yield return null;

                if (doSelect)
                {
                    doSelect = false;

                    this.cameraController.UpdateMode(CameraController.Mode.Stop);
                    this.characterController.AllowFire = false;

                    // 開啟選單
                    var charList = this.characterController.CharacterList;
                    var charSelector = this.levelUIManager.CharacterSelector;
                    charSelector.BeginSelect(charList);
                    while (charSelector.IsSelecting)
                    {
                        yield return null;
                    }


                    this.characterController.AllowFire = true;
                    this.cameraController.UpdateMode(CameraController.Mode.PlayerControl);

                    var newCharId = charSelector.CharaterId;
                    if (newCharId != GameDataManager.Instance.CharacterId)
                    {
                        charId = GameDataManager.Instance.CharacterId = newCharId;
                        // LevelManager.Singleton.ResetLevel();
                        //yield break;

                        goto SELECT_CHAR;
                    }

                    continue;
                }

                // 準備發射
                if (firing) {
                    this.levelUIManager.Mode = LevelUIManager.UIMode.None;
					this.cameraController.UpdateMode (CameraController.Mode.Stop);
				}

				// 取消?
				if (firingCancel) {
					firing = false;
					firingCancel = false;
                    this.levelUIManager.Mode = LevelUIManager.UIMode.WaitFiring;
                    this.cameraController.UpdateMode (CameraController.Mode.PlayerControl);
				}
			}
		}
			
		// 發射初始化
        this.levelUIManager.Mode = LevelUIManager.UIMode.WaitGoaling;

        var passed = false;
		var giveup = false;

		this.door.Passed += (sender, e) => {passed = true;};

		this.cameraController.UpdateMode (CameraController.Mode.FollowTrager);
		this.cameraController.UpdateTarget (this.characterController.Current);
		this.ClearTrails ();

		this.IsRunnig = true;

		// 等遊戲結束
		var endTime = Time.time + this.timeLimit;
		while (!passed) {
			yield return null;

			if (re) {
				re = false;
				giveup = true;
				this.showDie = false;
			}

			if ((Time.time >= endTime && !infinity) || died || giveup) {
				// 時間限制到，演出失敗動畫 & 重置場景
				this.door.enabled = false;
                this.KeepTrail();

                if (this.showDie) {
					yield return this.StartCoroutine (this.characterController.FailHandle ());
				}

				// Reset level
				this.ResetLevel ();
				yield break;
			}

			if (Input.GetKeyUp (KeyCode.R) || Input.GetKeyUp(KeyCode.Escape)) {
				giveup = true;
				this.showDie = false;
			} else if (Input.GetKeyUp (KeyCode.E)) {
				passed = true;
			}

			this.levelUIManager.SetCountDown(endTime - Time.time, infinity);
		}

		// 過關演出處理
		yield return this.characterController.PlayEndAnim(this.door.transform);

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

	void ClearTrails(){
		GameDataManager.Instance.ClearTrails ();
	}

	void KeepTrail(){

		var history = GameDataManager.Instance.HistoryTrails;
			
		// Only keep one trail
		foreach (var go in history.Where(v => v != null)) {
			GameObject.Destroy (go.gameObject);
		}

        history.Clear();

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