using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGround02 : MonoBehaviour {
    private bool move;
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animator.GetBool("isJump");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        print("OnMouseDown");
        move = animator.GetBool("isJump");
        print(move);
        if (move == false) animator.SetBool("isJump",true);
        else if (move == true) animator.SetBool("isJump", false);
    }

}
