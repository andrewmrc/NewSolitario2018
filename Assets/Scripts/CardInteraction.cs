using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CardInteraction : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public GameManager manager;

	public Transform parentToReturnTo = null;

    Vector3 startingPos;

    int tap;

    bool attached;


    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }


    //Gestisce il doppio tap
    public void OnPointerClick(PointerEventData eventData)
    {
        tap++;

        if (tap == 2)
        {
            print("DOUBLE TAP!");

            //In base alla posizione di partenza facciamo una serie di controlli per verificare la presenza di una posizione valida a cui agganciarsi

            //Controlliamo le carte che vengono dalla zona FIELD
            if (this.GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FIELD))
            {
                //Controlliamo se le carte da FIELD possono attaccarsi alla zona FINAL vuota
                for (int i = 0; i < manager.acePositions.Length; i++)
                {
                    if (!attached)
                    {
                        print("CONTROLLO AUTOMATICO ESEGUITO!");
                        SaveReferences();
                        EndActionConsequences(manager.acePositions[i]);
                    }
                    else
                    {
                        tap = 0;
                        attached = false;
                        return;
                    }
                }


                //Controlliamo se le carte da FIELD possono attaccarsi a qualche carta nella zona FINAL
                for (int i = 0; i < manager.shuffledDeck.Count; i++)
                {
                    if (manager.shuffledDeck[i].GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FINAL))
                    {
                        if (!attached)
                        {
                            print("CONTROLLO AUTOMATICO ESEGUITO!");
                            SaveReferences();
                            EndActionConsequences(manager.shuffledDeck[i]);
                        }
                        else
                        {
                            tap = 0;
                            attached = false;
                            return;
                        }
                    }
                }
            }


            //Controlliamo le carte che vengono dalla zona DRAW
            if (this.GetComponent<CardHandler>().cardPosition.Equals(CardPosition.DRAW)){

                //Controlliamo se le carte da DRAW possono attaccarsi alla zona FINAL vuota
                for (int i = 0; i < manager.acePositions.Length; i++)
                {
                    if (!attached)
                    {
                        print("CONTROLLO AUTOMATICO ESEGUITO!");
                        SaveReferences();
                        EndActionConsequences(manager.acePositions[i]);
                    }
                    else
                    {
                        tap = 0;
                        attached = false;
                        return;
                    }
                }


                //Controlliamo se le carte da posizione DRAW possono attaccarsi a qualche carta nella zona FINAL
                for (int i = 0; i < manager.shuffledDeck.Count; i++)
                {
                    if (manager.shuffledDeck[i].GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FINAL))
                    {
                        if (!attached)
                        {
                            print("CONTROLLO AUTOMATICO ESEGUITO!");
                            SaveReferences();
                            EndActionConsequences(manager.shuffledDeck[i]);
                        }
                        else
                        {
                            tap = 0;
                            attached = false;
                            return;
                        }
                    }
                }


                //Controlliamo se c'è un posto vuoto dove attaccare la K
                for (int i = 0; i < manager.fieldPositions.Length; i++)
                {
                    if (!attached)
                    {
                        print("CONTROLLO AUTOMATICO ESEGUITO!");
                        SaveReferences();
                        EndActionConsequences(manager.fieldPositions[i]);
                    }
                    else
                    {
                        tap = 0;
                        attached = false;
                        return;
                    }
                }


                //Controlliamo se le carte da DRAW possono attaccarsi a qualche carta in campo
                for (int i = 0; i < manager.shuffledDeck.Count; i++)
                {
                    if (manager.shuffledDeck[i].GetComponent<CardHandler>().cardStatus.Equals(CardStatus.UNCOVERED))
                    {
                        if (manager.shuffledDeck[i].GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FIELD))
                        {
                            if (manager.shuffledDeck[i].transform.childCount < 5)
                            {
                                if (!attached)
                                {
                                    print("CONTROLLO AUTOMATICO ESEGUITO!");
                                    SaveReferences();
                                    EndActionConsequences(manager.shuffledDeck[i]);
                                } else
                                {
                                    tap = 0;
                                    attached = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            tap = 0;

        }
    }


    //Gestisce l'esito positivo di un doppio tap
    public void SaveReferences ()
    {
        startingPos = this.transform.position;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.root);
    }


    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log ("OnBeginDrag");

        SaveReferences();

        GetComponent<CanvasGroup>().blocksRaycasts = false;
	}


	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");

		this.transform.position = eventData.position;
	}
	

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("OnEndDrag");
        
        GameObject objectHover = eventData.pointerCurrentRaycast.gameObject;

        EndActionConsequences(objectHover);

        //Fare una chiamata ad un metodo che controlla la situazione in campo o nella zona assi per vedere se si è vinta la partita

    }


    //Gestisce le conseguenze di una mossa (trascinamento o doppio tap)
    public void EndActionConsequences (GameObject objectToCheck)
    {
        //Controlliamo se l'oggetto sotto a quello su cui stiamo agendo è un'ogetto valido dove agganciarsi
        if (objectToCheck != null)
        {
            print("HOVERED: " + objectToCheck.name);
            if (objectToCheck.GetComponent<CardHandler>() != null)
            {
                //Se il suo valore è superiore di 1 al nostro ed è di colore opposto allora possiamo agganciarla altrimenti no
                if (objectToCheck.GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FIELD))
                {
                    if (!this.GetComponent<CardHandler>().cardColor.Equals(objectToCheck.GetComponent<CardHandler>().cardColor))
                    {
                        if (objectToCheck.GetComponent<CardHandler>().cardValue == (this.GetComponent<CardHandler>().cardValue + 1))
                        {
                            //Attacchiamo la carta alla nuova posizione
                            AttachCardHandler(objectToCheck, 60, CardPosition.FIELD);
                            return;
                        }
                    }
                } else if (objectToCheck.GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FINAL))
                {
                    //Controlliamo che la carta che stiamo trascinando nella zona assi non abbia altre carte attaccate come child
                    if (this.gameObject.transform.childCount < 5)
                    {
                        //Caso in cui stiamo droppando la carta nella zona degli assi
                        if (this.GetComponent<CardHandler>().cardType.Equals(objectToCheck.GetComponent<CardHandler>().cardType))
                        {
                            //Controlliamo se il valore della carta a cui vogliamo agganciare è minore di quello di questa
                            if (objectToCheck.GetComponent<CardHandler>().cardValue == (this.GetComponent<CardHandler>().cardValue - 1))
                            {
                                //Attacchiamo la carta alla nuova posizione
                                AttachCardHandler(objectToCheck.transform.parent.gameObject, 0, CardPosition.FINAL);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                //Se stiamo cercando di spostare una carta in una posizione vuota allora dovremo controllare se è una K e solo in quel caso la spostiamo
                if (objectToCheck.name.Contains("Posizione"))
                {
                    if (objectToCheck.transform.childCount == 0)
                    {
                        if (this.GetComponent<CardHandler>().cardValue == 13)
                        {
                            //Attacchiamo la carta alla nuova posizione
                            AttachCardHandler(objectToCheck, 0, CardPosition.FIELD);
                            return;
                        }
                    }
                }
                else
                {
                    //Se non è una posizione del field controlliamo che sia una posizione degli assi
                    if (objectToCheck.name.ToLower().Contains(this.GetComponent<CardHandler>().cardType.ToString().ToLower()))
                    {
                        if (objectToCheck.transform.childCount == 1)
                        {
                            if (this.GetComponent<CardHandler>().cardValue == 1)
                            {
                                //Attacchiamo la carta alla nuova posizione
                                AttachCardHandler(objectToCheck, 0, CardPosition.FINAL);
                                return;
                            } 
                        } 
                    } 
                }
            }
        }

        //Se la carta non può agganciarsi a nulla allora ritornerà alla sua posizione originaria
        print("RETURN TO POSITION!");
        ReturnCardToPosition();

    }


    //Gestisce l'aggancio di una carta ad una nuova posizione - Esegue l'esito di una mossa corretta
    public void AttachCardHandler (GameObject attachToThisObject, float offset, CardPosition position)
    {
        //Facciamo girare la carta lasciata scoperta se ce ne è almeno una
        if (parentToReturnTo.name.Contains("Posizione"))
        {
            if (parentToReturnTo.childCount > 0)
            {
                print("FLIP CARD COVERED!: " + parentToReturnTo.childCount);
                FlipUncoveredCard();
            }
        }


        //Se la carta viene dalla zona DRAW allora facciamo risistemare la zona DRAW al GameManager
        if (this.GetComponent<CardHandler>().cardPosition == CardPosition.DRAW)
        {
            manager.UseDrawCardConsequences();
        }

        //Imparentiamo la carta e la riposizioniamo come figlia 
        StartCoroutine(MoveCardToPosition(attachToThisObject.transform.position - new Vector3(0, offset, 0)));
        this.transform.SetParent(attachToThisObject.transform);
        this.GetComponent<CardHandler>().cardPosition = position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        //Questo bool deve essere messo a true solo se si arriva qui da un doppio tap
        if(tap == 2)
        {
            attached = true;
        } else
        {
            attached = false;
        }

        print("MOSSA CONSENTITA - CARTA ATTACCATA");
        manager.GameActionHandler();
    }


    //Gestisce il ritorno di una carta alla sua posizione di partenza
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


    //Scopre una carta coperta nel campo lasciata libera a seguito di una mossa corretta
    public void FlipUncoveredCard ()
    {
        print("CARTA GIRATA CON SUCCESSO!");
        GameObject lastChildInPosition = parentToReturnTo.GetChild(parentToReturnTo.childCount - 1).gameObject;
        print("Last Card In That Position: " + lastChildInPosition.name);
        lastChildInPosition.GetComponent<CardHandler>().cardStatus = CardStatus.UNCOVERED;
        lastChildInPosition.GetComponent<CardHandler>().CheckStatus();
    }

}
