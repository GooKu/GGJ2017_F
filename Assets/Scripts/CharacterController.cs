using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event System.EventHandler Fired;

    public Transform Current
    {
        get;
        private set;
    }

    public List<CharacterInfo> UnLockCharacterInfoList
    {
        get;
        private set;
    }

    void Awake()
    {
        // TODO: 僅註冊有使用到的物件
        var mds = GetComponentsInChildren<MouseDrag>();
        int unlockIndex = GameDataManager.Instance.GetUnlockIndexChracter();
        UnLockCharacterInfoList = new List<CharacterInfo>();
        foreach (var md in mds)
        {
            md.enabled = false;
            md.gameObject.SetActive(false);

            CharacterInfo info = md.GetComponent<CharacterInfo>();
            if (info.Id <= unlockIndex)
                UnLockCharacterInfoList.Add(info);
            else
                continue;

            md.GetComponent<MouseDrag>().Fired += this.OnFired;
        }
    }

    void OnFired(object sender, System.EventArgs e)
    {
        this.Current = (sender as MouseDrag).transform;

        if (this.Fired != null)
        {
            this.Fired(this, e);
        }
    }

    public void EnableCharacter(int id)
    {
        GameObject character = UnLockCharacterInfoList.Find(x => x.Id == id).gameObject;
        character.SetActive(true);
        character.GetComponent<MouseDrag>().enabled = true;
    }
}
