using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    public int GetUnlockIndexChracter()
    {
        PlayerPrefs.SetInt("ChracterUnlockIndex", 2);//gooku: tmp fot test
        return PlayerPrefs.GetInt("ChracterUnlockIndex", 1);
    }
}
