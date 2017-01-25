using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
	[SerializeField]
	private CharacterSelectUIView characterSelectUI;

	private void  Reset()
	{
		this.characterSelectUI = this.transform.GetComponentInChildren<CharacterSelectUIView>();
	}


	public bool IsSelecting {
		get;
		private set;
	}

	public void BeginSelect(CharacterInfo[] list){
		this.IsSelecting = true;
		this.characterSelectUI.Open (list);
	}

	public void EndSelect(int id){
		this.IsSelecting = false;
		this.CharaterId = id;
		this.characterSelectUI.Close();
	}

	public void EndSelect(GameObject item){
		this.IsSelecting = false;
		this.CharaterId = item.GetComponent<CharacterSelectItemView> ().SelectedId;
		this.characterSelectUI.Close();
	}

	public int CharaterId {
		get;
		private set;
	}

	// TODO:
}
