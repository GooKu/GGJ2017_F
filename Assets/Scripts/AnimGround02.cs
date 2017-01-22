using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGround02 : MonoBehaviour {
    private bool move;
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = transform.parent.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        move = !animator.GetBool("isJump");
        animator.SetBool("isJump", move);
        print(move);
    }

}
