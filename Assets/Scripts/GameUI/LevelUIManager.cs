using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField]
    private Text levelNumText;

    [SerializeField]
    private Text countDownText;

	[SerializeField]
	CharacterSelector characterSelector = null;

    [SerializeField]
    private Button returnBtn;

    [SerializeField]
    private Button selectionBtn;
    

    public event System.EventHandler ReturnButtonClicked;

    public event System.EventHandler SelectionButtonClicked;

    public enum UIMode
    {
        None,

        /// <summary>
        /// 等待球發射
        /// </summary>
        WaitFiring,

        /// <summary>
        /// 等待球進到門裡
        /// </summary>
        WaitGoaling,
    }

    UIMode mode;

    public UIMode Mode
    {
        get
        {
            return this.mode;
        }

        set
        {
            if (this.mode != value)
            {
                this.mode = value;
                this.returnBtn.gameObject.SetActive(value == UIMode.WaitGoaling);
                this.selectionBtn.gameObject.SetActive(value == UIMode.WaitFiring);
            }
        }
    }

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
        this.returnBtn.onClick.AddListener(this.OnReturnClick);
        this.selectionBtn.onClick.AddListener(this.OnSelectionClick);

        this.returnBtn.gameObject.SetActive(false);
        this.selectionBtn.gameObject.SetActive(false);

    }
		
	public void SetCountDown(float time, bool infinity)
    {
		if (infinity) {
			countDownText.text = "Level " + LevelManager.Singleton.CurrentLevel;
		} else {
			countDownText.text = "Level " + LevelManager.Singleton.CurrentLevel + "   " + string.Format ("{0:N2}", time);
		}
    }

    void OnReturnClick()
    {
        if (this.ReturnButtonClicked != null)
        {
            this.ReturnButtonClicked(this, System.EventArgs.Empty);
        }
    }

    void OnSelectionClick()
    {
        if (this.SelectionButtonClicked != null)
        {
            this.SelectionButtonClicked(this, System.EventArgs.Empty);
        }
    }
}
