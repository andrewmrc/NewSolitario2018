using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public GameManager manager;

	public Transform parentToReturnTo = null;
    Vector3 startingPos;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
		//Debug.Log ("OnBeginDrag");

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

        EndDragConsequences(eventData);

        //Fare una chiamata ad un metodo che controlla la situazione in campo o nella zona assi per vedere se si è vinta la partita

	}


    public void EndDragConsequences (PointerEventData lastEventData)
    {
        GameObject objectHover = lastEventData.pointerCurrentRaycast.gameObject;

        //Controlliamo se la carta sotto a quella che stiamo trascinando è una carta valida per essere agganciata
        //Se il suo valore è superiore di 1 al nostro ed è di colore opposto allora possiamo agganciarla altrimenti no
        if (objectHover != null)
        {
            print("HOVERED: " + objectHover.name);
            if (objectHover.GetComponent<CardHandler>() != null)
            {

                if (objectHover.GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FIELD))
                {
                    if (!this.GetComponent<CardHandler>().cardColor.Equals(objectHover.GetComponent<CardHandler>().cardColor))
                    {
                        if (objectHover.GetComponent<CardHandler>().cardValue == (this.GetComponent<CardHandler>().cardValue + 1))
                        {

                            //Facciamo girare la carta lasciata scoperta se ce ne è almeno una
                            if (parentToReturnTo.name.Contains("Posizione"))
                            {
                                if (parentToReturnTo.childCount > 0)
                                {
                                    print("FLIP UNCOVERED CLASSIC");
                                    FlipUncoveredCard();
                                }
                            }


                            //Se la carta viene dalla zona DRAW allora facciamo risistemare la zona DRAW al GameManager
                            if (this.GetComponent<CardHandler>().cardPosition == CardPosition.DRAW)
                            {
                                manager.UseDrawCardConsequences();
                            }

                            //Imparentiamo la carta e la riposizioniamo come figlia della precedente
                            StartCoroutine(MoveCardToPosition(objectHover.transform.position - new Vector3(0, 60, 0)));
                            this.transform.SetParent(objectHover.transform);
                            this.GetComponent<CardHandler>().cardPosition = CardPosition.FIELD;
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
                } else if (objectHover.GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FINAL))
                {
                    print("ABBIAMO SOTTO UNA CARTA DI TIPO FINAL!");
                    //Controlliamo che la carta che stiamo trascinando nella zona assi non abbia altre carte attaccate come child <-------------
                    if (this.gameObject.transform.childCount < 5)
                    {
                        print("NON ABBIAMO CHILD SOTTO LA CARTA TRASCINATA!");
                        //Caso in cui stiamo droppando la carta nella zona degli assi
                        if (this.GetComponent<CardHandler>().cardType.Equals(objectHover.GetComponent<CardHandler>().cardType))
                        {
                            print("TIPO CARTA CORRISPONDE!");
                            if (objectHover.GetComponent<CardHandler>().cardValue == (this.GetComponent<CardHandler>().cardValue - 1))
                            {
                                print("VALORE CARTA SOTTO MINORE!");

                                //Facciamo girare la carta lasciata scoperta se ce ne è almeno una
                                if (parentToReturnTo.name.Contains("Posizione"))
                                {
                                    if (parentToReturnTo.childCount > 0)
                                    {
                                        print("FLIP UNCOVERED CLASSIC");
                                        FlipUncoveredCard();
                                    }
                                }


                                //Se la carta viene dalla zona DRAW allora facciamo risistemare la zona DRAW al GameManager
                                if (this.GetComponent<CardHandler>().cardPosition == CardPosition.DRAW)
                                {
                                    manager.UseDrawCardConsequences();
                                }

                                //Imparentiamo la carta e la riposizioniamo come figlia della precedente
                                StartCoroutine(MoveCardToPosition(objectHover.transform.parent.position));
                                this.transform.SetParent(objectHover.transform.parent);
                                this.GetComponent<CardHandler>().cardPosition = CardPosition.FINAL;
                                GetComponent<CanvasGroup>().blocksRaycasts = true;
                            }
                            else
                            {
                                print("VALORE CARTA SOTTO NON E' MINORE!");
                                //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                                ReturnCardToPosition();
                            }
                        }
                        else
                        {
                            print("TIPO CARTA NON CORRISPONDE!");
                            //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                            ReturnCardToPosition();
                        }
                    }
                    else
                    {
                        print("ABBIAMO CHILD SOTTO LA CARTA TRASCINATA!");

                        //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                        ReturnCardToPosition();
                    }
                }
                else
                {
                    print("NON ABBIAMO SOTTO UNA CARTA DI TIPO FINAL!");

                    //Se la carta non può agganciarsi a quella su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                    ReturnCardToPosition();
                }
            }
            else
            {
                //Se stiamo cercando di spostare una carta in una posizione vuota allora dovremo controllare se è una K e solo in quel caso spostarla
                if (objectHover.name.Contains("Posizione"))
                {
                    if (objectHover.transform.childCount == 0)
                    {
                        if (this.GetComponent<CardHandler>().cardValue == 13)
                        {
                            print("CASO KKKKKK");
                            //Facciamo girare la carta lasciata scoperta se ce ne è almeno una
                            if (parentToReturnTo.name.Contains("Posizione"))
                            {
                                if (parentToReturnTo.childCount > 0)
                                {
                                    print("FLIP UNCOVERED 13");
                                    FlipUncoveredCard();
                                }

                            }


                            //Se la carta viene dalla zona DRAW allora facciamo risistemare la zona DRAW al GameManager
                            if (this.GetComponent<CardHandler>().cardPosition == CardPosition.DRAW)
                            {
                                manager.UseDrawCardConsequences();
                            }

                            //Imparentiamo la carta e la riposizioniamo come figlia della precedente
                            StartCoroutine(MoveCardToPosition(objectHover.transform.position));
                            this.transform.SetParent(objectHover.transform);
                            this.GetComponent<CardHandler>().cardPosition = CardPosition.FIELD;
                            GetComponent<CanvasGroup>().blocksRaycasts = true;
                        }
                        else
                        {
                            //Se la carta non può agganciarsi all'oggetto su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                            ReturnCardToPosition();
                        }
                    }
                    else
                    {
                        //Se la carta non può agganciarsi all'oggetto su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                        ReturnCardToPosition();
                    }
                }
                else
                {
                    //Se non è una posizione del field controlliamo che sia una posizione degli assi  <-------------
                    if (objectHover.name.ToLower().Contains(this.GetComponent<CardHandler>().cardType.ToString().ToLower()))
                    {
                        if (objectHover.transform.childCount == 1)
                        {
                            if (this.GetComponent<CardHandler>().cardValue == 1)
                            {

                                //Facciamo girare la carta lasciata scoperta se ce ne è almeno una
                                if (parentToReturnTo.name.Contains("Posizione"))
                                {
                                    if (parentToReturnTo.childCount > 0)
                                    {
                                        print("FLIP UNCOVERED CLASSIC");
                                        FlipUncoveredCard();
                                    }
                                }


                                //Se la carta viene dalla zona DRAW allora facciamo risistemare la zona DRAW al GameManager
                                if (this.GetComponent<CardHandler>().cardPosition == CardPosition.DRAW)
                                {
                                    manager.UseDrawCardConsequences();
                                }

                                //Imparentiamo la carta e la riposizioniamo come figlia 
                                StartCoroutine(MoveCardToPosition(objectHover.transform.position));
                                this.transform.SetParent(objectHover.transform);
                                this.GetComponent<CardHandler>().cardPosition = CardPosition.FINAL;
                                GetComponent<CanvasGroup>().blocksRaycasts = true;

                            } else
                            {
                                //Se la carta non può agganciarsi all'oggetto su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                                ReturnCardToPosition();
                            }

                        } else
                        {
                            //Se la carta non può agganciarsi all'oggetto su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                            ReturnCardToPosition();
                        }

                    } else
                    {
                        //Se la carta non può agganciarsi all'oggetto su cui sta venendo droppata allora ritornerà alla sua posizione originaria
                        ReturnCardToPosition();
                    }
                }
            }
        }
        else
        {
            //Se la carta non può agganciarsi a nulla allora ritornerà alla sua posizione originaria
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


    public void FlipUncoveredCard ()
    {       
        GameObject lastChildInPosition = parentToReturnTo.GetChild(parentToReturnTo.childCount - 1).gameObject;
        print("Last Card In That Position: " + lastChildInPosition.name);
        lastChildInPosition.GetComponent<CardHandler>().cardStatus = CardStatus.UNCOVERED;
        lastChildInPosition.GetComponent<CardHandler>().CheckStatus();
    }

}
