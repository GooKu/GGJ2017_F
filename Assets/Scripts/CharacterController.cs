using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Sprite failSprite;

	/// <summary>
	/// 飛出去
	/// </summary>
    public event System.EventHandler Fired;

	/// <summary>
	/// 掛了
	/// </summary>
	public event System.EventHandler Died;

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

    private bool isPlayingStartAnim;

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
        transform.Find("StartAnim").gameObject.SetActive(false);
    }

    void OnFired(object sender, System.EventArgs e)
    {
        this.Current = (sender as MouseDrag).transform;
        (sender as MouseDrag).enabled = false;

        if (this.Fired != null)
        {
            this.Fired(this, e);
        }
        Current.GetComponent<CharacterChecker>().EnableStilCheck(this.OnStill);
    }

    void OnStill(object sender, System.EventArgs e)
    {
		// TODO: 
        this.Current = (sender as CharacterChecker).transform;

        if (this.Fired != null)
        {
           // this.Still(this, e);
        }
    }

	public IEnumerator FailHandle()
    {
        if (failSprite == null)
			yield break;
        Current.GetComponent<Rigidbody2D>().isKinematic = true;
        Current.GetComponent<SpriteRenderer>().sprite = failSprite;

		yield return new WaitForSeconds (2f);
    }

    public void EnableCharacter(int id, BoxCollider2D boundary)
    {
        GameObject character = UnLockCharacterInfoList.Find(x => x.Id == id).gameObject;
        character.SetActive(true);
        character.GetComponent<MouseDrag>().enabled = true;
        character.GetComponent<BoundcinessController>().SetBoundary(boundary);

		// TODO:
        // character.GetComponent<BoundcinessController>().OutOfBounds += outOfBounds;
    }

	public IEnumerator PlayStartAnim(int charId)
    {
		GameObject animObj = transform.Find("StartAnim").gameObject;
		animObj.SetActive(true);

		while(isPlayingStartAnim)
			yield return null;

		animObj.SetActive(false);
    }

    public void StartAnimPlay()
    {
        isPlayingStartAnim = true;
    }

    public void StartAnimFinish()
    {
        isPlayingStartAnim = false;
    }
}
