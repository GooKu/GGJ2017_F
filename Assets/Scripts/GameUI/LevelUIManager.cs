using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField]
    private CharacterSelectUIView characterSelectUI;

    [SerializeField]
    private Text countDownText;

    private void  Reset()
    {
        this.characterSelectUI = this.transform.GetComponentInChildren<CharacterSelectUIView>();
        this.countDownText = this.transform.GetComponentInChildren<Text>();
    }

    public void Init(List<CharacterInfo> characterList)
    {
        characterSelectUI.Open(characterList);
        UpdateCountDownText(0);
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void GameStart(float time)
    {
        characterSelectUI.Close();
        UpdateCountDownText(time);
    }

    public void UpdateCountDownText(float time)
    {
        countDownText.text = string.Format("{0:N2}", time);
    }
}
