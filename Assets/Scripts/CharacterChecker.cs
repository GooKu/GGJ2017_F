using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChecker : MonoBehaviour
{
    public event System.EventHandler Still;

    private static readonly float stillCheckTime = 1;
    private float currentCheckTime;
    private bool isStillCheck = false;
    private static readonly Vector2 stillCheckVect = new Vector2(60f, 60f);
    private Rigidbody2D rigidbody;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isStillCheck)
            stillCheck();
    }

    private void stillCheck()
    {
        Debug.Log(rigidbody.velocity.magnitude);
        Debug.Log(stillCheckVect.magnitude);
        Debug.Log(Mathf.Min(rigidbody.velocity.magnitude, stillCheckVect.magnitude));
        Debug.Log(currentCheckTime);

        if (rigidbody.velocity.magnitude != Mathf.Min(rigidbody.velocity.magnitude, stillCheckVect.magnitude))
        {
            currentCheckTime = 0;
            return;
        }

        currentCheckTime += Time.deltaTime;

        if (currentCheckTime < stillCheckTime)
            return;

        isStillCheck = false;
        if (Still != null)
            Still(this, System.EventArgs.Empty);

    }

    public void EnableStilCheck( System.EventHandler Still)
    {
        isStillCheck = true;
        currentCheckTime = 0;
        this.Still += Still;
    }
}
