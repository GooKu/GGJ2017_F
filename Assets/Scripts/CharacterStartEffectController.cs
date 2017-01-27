using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStartEffectController : MonoBehaviour {

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Vector2 targetOffset = new Vector2(0, +47.2996f);

    [SerializeField]
    float initWait = 0.45f;

    [SerializeField]
    float jumpSpeed = 10;

    [SerializeField]
    SpriteRenderer ballSprite;

    [SerializeField]
    Sprite[] ballSprites;

    Animator anim;
    SpriteRenderer sprite;

    void Awake()
    {
        this.anim = this.GetComponent<Animator>();
        this.sprite = this.GetComponent<SpriteRenderer>();

        this.sprite.enabled = false;
        this.ballSprite.enabled = false;

        // 計算初始位置
        var ray = new Ray(this.transform.position, Vector3.down);
        var hit = Physics2D.Raycast(this.transform.position, Vector2.down, float.PositiveInfinity, groundLayer.value);
        if (hit.collider == null)
        {
            // TODO: No hit?
            Debug.Log("No Ground? Check it please");
        }
        else
        {
            var pos = this.transform.position;
            pos.x = hit.point.x + targetOffset.x;
            pos.y = hit.point.y + targetOffset.y;
            this.transform.position = pos;
        }

    }

    public IEnumerator Play(int charId)
    {

        // 動畫時間計算
        this.sprite.enabled = true;

        var d = Mathf.Abs(this.transform.localPosition.y);
        var t = d / Mathf.Max(this.jumpSpeed, 0.001f);

        this.anim.Play("Idle");
        yield return new WaitForSeconds(initWait);
        this.anim.SetTrigger("Start");

        this.sprite.sprite = this.ballSprites[charId];

        while (true)
        {
            var info = this.anim.GetCurrentAnimatorStateInfo(0);
            if (!info.IsName("Ready") && !info.IsName("Idle"))
            {
                break;
            }

            yield return null;
        }

        var startTime = Time.time;
        var endTime = startTime + t;
        var startPos = this.transform.localPosition;
        var endPos = Vector3.zero;

        while (Time.time < endTime)
        {
            var v = Mathf.InverseLerp(startTime, endTime, Time.time);
            var pos = Vector3.Lerp(startPos, endPos, v);
            this.transform.localPosition = pos;

            yield return null;
        }

        this.transform.localPosition = endPos;

        this.anim.SetTrigger("End");

        while (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("None"))
        {
            yield return null;

            this.ballSprite.enabled = true;

            var c = this.ballSprite.color;
            c.a = 1 - this.sprite.color.a;
            this.ballSprite.color = c;
        }

        this.ballSprite.enabled = false;
        this.sprite.enabled = false;
    }
}
