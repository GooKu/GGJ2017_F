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

	public CharacterSelector CharacterSelector{
		get{
			return this.characterSelector;
		}
		
	}

    private void Reset()
    {
		this.characterSelector = this.transform.GetComponentInChildren<CharacterSelector>();
        this.countDownText = this.transform.GetComponentInChildren<Text>();
    }

	void Awake()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}
		
	public void SetCountDown(float time, bool infinity)
    {
		if (infinity) {
			countDownText.text = "Infinity";
		} else {
			countDownText.text = string.Format ("{0:N2}", time);
		}
    }
}
