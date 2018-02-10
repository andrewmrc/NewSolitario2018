using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    GameManager manager;

	public Transform parentToReturnTo = null;
    Vector3 startingPos;

    public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");

        startingPos = this.transform.position;
		parentToReturnTo = this.transform.parent;
		this.transform.SetParent( this.transform.root );

		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");

		this.transform.position = eventData.position;
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("OnEndDrag");

        GameObject objectHover = eventData.pointerCurrentRaycast.gameObject;
        //print("HOVERED: " + objectHover.name);

        //Controlliamo se la carta sotto a quella che stiamo trascinando è una carta valida per essere agganciata
        //Se il suo valore è superiore di 1 al nostro ed è di colore opposto allora possiamo agganciarla altrimenti no

        if (objectHover != null /*|| objectHover.GetComponent<CardHandler>() != null*/)
        {
            if (objectHover.GetComponent<CardHandler>() != null)
            {
                if (!this.GetComponent<CardHandler>().colorOfCard.Equals(objectHover.GetComponent<CardHandler>().colorOfCard))
                {
                    if (objectHover.GetComponent<CardHandler>().cardValue == (this.GetComponent<CardHandler>().cardValue + 1))
                    {
                        //Se stiamo cercando di spostare una carta in una posizione vuota allora dovremo controllare se è una K e solo in quel caso spostarla



                        //Facciamo girare la carta lasciata scoperta se ce ne è almeno una
                        if (parentToReturnTo.childCount > 0)
                        {
                            GameObject lastChildInPosition = parentToReturnTo.GetChild(parentToReturnTo.childCount - 1).gameObject;
                            print("Last Card In That Position: " + lastChildInPosition.name);
                            lastChildInPosition.GetComponent<CardHandler>().FlipCard(true);
                        }



                        //Se lo è imparentiamo la carta e la riposizioniamo come figlia della precedente
                        StartCoroutine(MoveCardToPosition(objectHover.transform.position - new Vector3(0, 60, 0)));
                        this.transform.SetParent(objectHover.transform);
                        GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    else
                    {
                        //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                        ReturnCardToPosition();
                    }
                }
                else
                {
                    //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                    ReturnCardToPosition();
                }
            }
            else
            {
                //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                ReturnCardToPosition();
            }
        }
        else
        {
            //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
            ReturnCardToPosition();
        }
	}


    public void ReturnCardToPosition ()
    {
        StartCoroutine(MoveCardToPosition(startingPos));
        this.transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    //Metodo per traslare una carta da una posizione ad un'altra
    public IEnumerator MoveCardToPosition(Vector3 newPos)
    {
        float timeToComplete = 0.05f;

        var lastPos = this.transform.position;
        float currTime = 0f;
        float remainingTimePerc = 0f;

        while (currTime < timeToComplete)
        {
            currTime += Time.deltaTime;
            remainingTimePerc += Time.deltaTime / timeToComplete;
            this.transform.position = Vector3.Lerp(lastPos, newPos, currTime / timeToComplete);
            yield return null;
        }

    }

}
