using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour {

    public string segno;
    public int cardValue;

    public Animator anim;

	// Use this for initialization
	void Awake () {
        anim = this.GetComponent<Animator>();
        //FlipCard(true);
    }
	
	// Update is called once per frame
	void Update () {

    }


    public void FlipCard (bool flipDirection)
    {
        if (flipDirection)
        {
            anim.SetBool("flip", true);
        } else
        {
            anim.SetBool("flip", false);
        }
    }


}
