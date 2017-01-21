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

	[SerializeField]
	CharacterController characterController;

	[SerializeField]
	CameraController cameraController;

	[SerializeField]
	BoxCollider2D cameraRegion;

    [SerializeField]
    private LevelUIManager levelUIManager;

    void Reset(){
		this.characterController = this.transform.GetComponentInChildren<CharacterController> ();
		this.cameraController = this.transform.GetComponentInChildren<CameraController> ();
        this.cameraRegion = this.GetComponent<BoxCollider2D>();
        this.levelUIManager = this.GetComponentInChildren<LevelUIManager>();
    }

    void Awake(){
		
	}

	void Start()
	{
		this.cameraController.UpdateMode (CameraController.Mode.Stop);
		this.cameraController.Init (this.characterController.transform, this.cameraRegion);

		this.cameraRegion.enabled = false;
		this.characterController.Fired += this.OnFired;
        this.levelUIManager.Init(this.characterController.UnLockCharacterInfoList);
    }

	void OnFired (object sender, System.EventArgs e)
	{
		this.DoRun ();
	}

	void DoRun()
	{
		if (this.IsRunnig) {
			return;
		}

		// TODO: 其他開始 Level 的動作
		this.cameraController.UpdateMode (CameraController.Mode.FollowTrager);
		this.cameraController.UpdateTarget (this.characterController.Current);

		this.IsRunnig = true;
	}

    public void OnSelectCharacter(GameObject item)
    {
        this.levelUIManager.GameStart();
        this.cameraController.UpdateMode(CameraController.Mode.PlayerControl);
        this.characterController.EnableCharacter(item.GetComponent<CharacterSelectItemView>().SelectedId);
    }
}

public static class LevelControllerUtility{
	public static LevelController GetLevelController(this GameObject go){
		return go.transform.root.GetComponent<LevelController> ();
	}
}