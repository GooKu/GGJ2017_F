using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField]
    private CharacterSelectUIView characterSelectUI;

    void Reset()
    {
        this.characterSelectUI = this.transform.GetComponentInChildren<CharacterSelectUIView>();
    }

    public void Init(List<CharacterInfo> characterList)
    {
        characterSelectUI.Open(characterList);
    }

    public void GameStart()
    {
        characterSelectUI.Close();
    }
}
