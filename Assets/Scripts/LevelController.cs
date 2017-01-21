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

	void Reset(){
		this.characterController = this.transform.GetComponentInChildren<CharacterController> ();
		this.cameraController = this.transform.GetComponentInChildren<CameraController> ();
		this.cameraRegion = this.GetComponent<BoxCollider2D> ();
	}

	void Awake(){
		
	}

	void Start()
	{
		this.cameraController.UpdateMode (CameraController.Mode.PlayerControl);
		this.cameraController.Init (this.starter.transform, this.cameraRegion);

		this.cameraRegion.enabled = false;
		this.characterController.Fired += this.OnFired;
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

		this.IsRunnig = true;
	}
}

public static class LevelControllerUtility{
	public static LevelController GetLevelController(this GameObject go){
		return go.transform.root.GetComponent<LevelController> ();
	}
}