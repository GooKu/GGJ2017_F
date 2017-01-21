using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	//public event System.EventHandler RunStarted;

	//private bool run = false;

	//public bool IsRunnig {
	//	get{
	//		return this.run;
	//	}
	//	private set{
	//		if (this.run != value) {
	//			this.run = value;

	//			if (value) {
	//				if (this.RunStarted != null) {
	//					this.RunStarted (this, System.EventArgs.Empty);
	//				}
	//			}
	//		}
	//	}
	//}

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

    [SerializeField]
    private float timeLimit = 15;

    private float countDownTime;

    private enum GameStep
    {
        Non,
        OnSelectCharacter,
        WaitForFire,
        OnRunning,
        Pass,
        DoFail,
        Fail
    }
    private GameStep currentStep = GameStep.Non;

    void Reset(){
		this.characterController = this.transform.GetComponentInChildren<CharacterController> ();
		this.cameraController = this.transform.GetComponentInChildren<CameraController> ();
        this.cameraRegion = this.GetComponent<BoxCollider2D>();
        this.levelUIManager = this.GetComponentInChildren<LevelUIManager>();
        this.door = this.GetComponentInChildren<Door>();
    }

	void Start()
	{
        this.cameraController.UpdateMode (CameraController.Mode.Stop);
		this.cameraController.Init (this.characterController.transform, this.cameraRegion);
        this.cameraRegion.enabled = false;
		this.characterController.Fired += this.OnFired;
        this.levelUIManager.Init(this.characterController.UnLockCharacterInfoList);
        this.door.Pass += OnPass;
        currentStep = GameStep.WaitForFire;
    }

    private void Update()
    {
        switch (currentStep)
        {
            case GameStep.OnRunning:
                countDownTime -= Time.deltaTime;
                if (countDownTime <= 0)
                {
                    countDownTime = 0;
                    currentStep = GameStep.DoFail;
                }
                levelUIManager.UpdateCountDownText(countDownTime);
                break;
            case GameStep.DoFail:
                if (LevelManager.Singleton != null)
                    LevelManager.Singleton.ResetLevel();
                else {
                    Debug.Log("必須從 Main 開始執行才能重來");
                    currentStep = GameStep.Non;
                }

                break;
        }
    }

    void OnFired (object sender, System.EventArgs e)
	{
		this.DoRun ();
	}

	void DoRun()
	{
        //if (!IsRunnig)
        //    return;

		if (currentStep != GameStep.WaitForFire) 
			return;

		// TODO: 其他開始 Level 的動作
		this.cameraController.UpdateMode (CameraController.Mode.FollowTrager);
		this.cameraController.UpdateTarget (this.characterController.Current);

        //this.IsRunnig = true;
        currentStep =  GameStep.OnRunning;
    }

    public void OnSelectCharacter(GameObject item)
    {
        countDownTime = timeLimit;
        this.levelUIManager.GameStart(countDownTime);
        this.cameraController.UpdateMode(CameraController.Mode.PlayerControl);
        this.cameraRegion.enabled = true;//gooku: tmp enable for get correct  bounds;
        this.characterController.EnableCharacter(item.GetComponent<CharacterSelectItemView>().SelectedId, this.cameraRegion, OutOfBoundss);
        this.cameraRegion.enabled = false;
    }

    void OutOfBoundss(object sender, System.EventArgs e)
    {
        currentStep = GameStep.DoFail;
    }

    void OnPass(object sender, System.EventArgs e)
    {
        currentStep = GameStep.Pass;

        if (LevelManager.Singleton != null)
        {
            LevelManager.Singleton.NextLevel();
        }
        else
        {
            Debug.Log("必須從 Main 開始執行才能下一關");
        }
    }
}

public static class LevelControllerUtility{
	public static LevelController GetLevelController(this GameObject go){
		return go.transform.root.GetComponent<LevelController> ();
	}
}