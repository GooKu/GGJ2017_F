using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public AudioClip myAuioClip;
    [SerializeField]
    private Sprite failSprite;

    [SerializeField]
    private CharacterStartEffectController startEffect;

    [SerializeField]
    private CharacterInfo[] characters = new CharacterInfo[0];

    public CharacterInfo[] CharacterList
    {
        get
        {
            return this.characters;
        }
    }

    bool allowFire = true;
    public bool AllowFire
    {
        get
        {
            return this.allowFire;
        }

        set
        {
            if (this.allowFire != value)
            {
                this.allowFire = value;

                if (this.Current != null)
                {
                    var md = this.Current.GetComponent<MouseDrag>();
                    md.enabled = value;
                }
            }
        }
    }

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

    void Awake()
    {
        // TODO: 僅註冊有使用到的物件
        foreach (var charInfo in this.characters)
        {
            var md = charInfo.GetComponent<MouseDrag>();
            md.enabled = false;
            md.gameObject.SetActive(false);
            md.Fired += this.OnFired;
            md.Firing += this.OnFiring;
            md.FiringCancel += this.OnFiringCancel;

            var checker = charInfo.GetComponent<CharacterChecker>();
            checker.OutOfBounds += this.OnOutOfBounds;
        }

        for (var i = 0; i < this.characters.Length; i++)
        {
            this.characters[i].Id = i;
        }
    }

    void OnFired(object sender, System.EventArgs e)
    {
        (sender as MouseDrag).enabled = false;

        if (this.Fired != null)
        {
            this.Fired(this, e);
        }

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

    public IEnumerator FailHandle()
    {
        if (failSprite == null)
            yield break;

        Current.GetComponent<SpriteRenderer>().sprite = failSprite;
        gameObject.GetComponent<AudioSource>().PlayOneShot(myAuioClip, 0.1f);

        yield return new WaitForSeconds(2f);
    }

    public void EnableCharacter(int id, BoxCollider2D boundary)
    {
        if (id < 0 || id >= this.characters.Length)
        {
            throw new System.ArgumentNullException("id");
        }

        if (this.Current != null)
        {
            this.Current.gameObject.SetActive(false);
        }

        var character = this.characters[id].gameObject;
        character.GetComponent<MouseDrag>().enabled = true;
        character.GetComponent<CharacterChecker>().SetBoundary(boundary);

        this.Current = character.transform;
        this.Current.gameObject.SetActive(true);
    }

    private void OnOutOfBounds(object sender, System.EventArgs e)
    {
        var rigid = this.Current.GetComponent<Rigidbody2D>();
        rigid.velocity = -rigid.velocity;

        if (this.Died != null)
        {
            this.Died(this, e);
        }
    }

    public IEnumerator PlayStartAnim(int charId)
    {
        this.startEffect.gameObject.SetActive(true);

        var itr = this.startEffect.Play(charId);
        while (itr.MoveNext())
        {
            yield return null;
        }

        this.startEffect.gameObject.SetActive(false);
    }

    public IEnumerator PlayEndAnim(Transform doorTrans)
    {
        float time = 1.5f;

        var startTime = Time.time;
        var endTime = startTime + time;

        var rigid = Current.GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;
        rigid.velocity = Vector2.zero;
        rigid.angularDrag = 0;
        rigid.angularVelocity = 0;

        do
        {
            var s = Mathf.InverseLerp(startTime, endTime, Time.time);
            Current.position = Vector3.Lerp(Current.position, doorTrans.position, s);
            Current.localScale = Vector3.Lerp(Current.localScale, new Vector3(.01f, .01f, .01f), s);
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
