using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{

    [SerializeField]
    private Text countDownText;

	[SerializeField]
	CharacterSelector characterSelector = null;

    [SerializeField]
    private Button returnBtn;

    public event System.EventHandler Return;

    public CharacterSelector CharacterSelector{
		get{
			return this.characterSelector;
		}
		
	}

    private void Reset()
    {
		this.characterSelector = this.transform.GetComponentInChildren<CharacterSelector>();
        this.countDownText = this.transform.GetComponentInChildren<Text>();
        this.returnBtn = this.transform.GetComponentInChildren<Button>();
    }

	void Awake()
	{
		this.countDownText.text = string.Empty;
	}
		
	public void SetCountDown(float time, bool infinity)
    {
		if (infinity) {
			countDownText.text = "Infinity";
		} else {
			countDownText.text = string.Format ("{0:N2}", time);
		}
    }

    public void ShowReturnBtn(bool isShow)
    {
        this.returnBtn.gameObject.SetActive(isShow);
    }

    public void OnReturnClick()
    {
        this.Return(this, System.EventArgs.Empty);
    }
}
