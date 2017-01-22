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
    public event System.EventHandler Firing;
    public event System.EventHandler FiringCancel;

    /// <summary>
    /// 掛了
    /// </summary>
    public event System.EventHandler Died;

    public Transform Current
    {
        get;
        private set;
    }

	public TrailController CurrentTrail{
		get{
			if (this.Current != null) {
				return this.Current.GetComponentInChildren<TrailController> ();
			}
			return null;
		}
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
            md.GetComponent<MouseDrag>().Firing += this.OnFiring;
            md.GetComponent<MouseDrag>().FiringCancel += this.OnFiringCancel;
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

    void OnFiring(object sender, System.EventArgs e)
    {
        if (this.Firing != null)
        {
            this.Firing(this, e);
        }
    }

    void OnFiringCancel(object sender, System.EventArgs e)
    {
        if (this.FiringCancel != null)
        {
            this.FiringCancel(this, e);
        }
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

        yield return new WaitForSeconds(2f);
    }

    public void EnableCharacter(int id, BoxCollider2D boundary)
    {
        GameObject character = UnLockCharacterInfoList.Find(x => x.Id == id).gameObject;
        character.SetActive(true);
        character.GetComponent<MouseDrag>().enabled = true;
        CharacterChecker checker = character.GetComponent<CharacterChecker>();
        checker.SetBoundary(boundary);
        checker.OutOfBounds += this.Died;
    }

    public IEnumerator PlayStartAnim(int charId)
    {
        GameObject animObj = transform.Find("StartAnim").gameObject;
        animObj.SetActive(true);

        while (isPlayingStartAnim)
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

    public IEnumerator PlayEndAnim(Transform doorTrans)
    {
        float time = 1.5f;

        var startTime = Time.time;
        var endTime = startTime + time;

        Current.GetComponent<Rigidbody2D>().isKinematic = true;

        do
        {
            var s = Mathf.InverseLerp(startTime, endTime, Time.time);
            Current.position = Vector3.Lerp(Current.position, doorTrans.position, s);
            Current.localScale = Vector3.Lerp(Current.localScale, new Vector3(.1f, .1f, .1f), s);
            yield return null;
        } while (Time.time < endTime);

          Current.localScale = Vector3.zero;
    }

    public void RecoverCurrent()
    {
        if (Current == null)
            return;

        Current.localPosition = Vector3.zero;
        Current.localScale = Vector3.one;
        Current.gameObject.SetActive(false);
        transform.Find("StartAnim").gameObject.SetActive(false);
    }
}
