using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardSymbol { NULL, CUORI, QUADRI, FIORI, PICCHE }

public enum CardColor { NULL, ROSSO, NERO }

public enum CardPosition { DECK, DRAW, FIELD, FINAL }

public enum CardStatus { COVERED, UNCOVERED }

public class CardHandler : MonoBehaviour {

    public CardColor cardColor = CardColor.NULL;
    
    public CardSymbol cardType = CardSymbol.NULL;

    public CardPosition cardPosition = CardPosition.DECK;

    public CardStatus cardStatus = CardStatus.COVERED;

    public int cardValue;

    public Animator anim;

	// Use this for initialization
	void Awake () {
        anim = this.GetComponent<Animator>();
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

    public void CheckStatus ()
    {
        if(this.cardStatus == CardStatus.UNCOVERED)
        {
            FlipCard(true);
        } else if(this.cardStatus == CardStatus.COVERED)
        {
            FlipCard(false);
        }
    }




}
