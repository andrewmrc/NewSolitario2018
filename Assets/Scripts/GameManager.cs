using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score, gameAction;

    public Text scoreText, gameActionText, scoreTextGO, gameActionTextGO;

    public GameObject drawButton, cardPrefab, cardToField, gameOverPanel;

    public Transform deckPosition;

    public GameObject[] fieldPositions;

    public GameObject[] acePositions;

    public GameObject[] drawCardsPositions;

    public Sprite[] cardValues;

    public Sprite[] cardSemi;


    public GameObject[] orderedDeck;

    public List<GameObject> shuffledDeck;

    public List<GameObject> cardsOnField;

    public List<GameObject> cardsInDeck;

    bool exitToCoro, isVictory, stopCheck;

    float remainingTimePerc;

    // Use this for initialization
    void Start()
    {
        orderedDeck = new GameObject[52];
        CreateDeck();
    }


    public void LoadScene (int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    //Metodo per testare la creazione dinamica del mazzo posizionando ogni carta in ordine sul proprio seme nella zona assi e facendole girare
    public void CreateDeckDebug()
    {
        int nCards = 0;
        int offsetPosition = 0;
        for (int i = 0; i < acePositions.Length; i++)
        {
            offsetPosition = 0;
            for (int j = 0; j < 13; j++)
            {
                GameObject newCard = Instantiate(cardPrefab);
                orderedDeck[nCards] = newCard;
                nCards++;
                newCard.transform.SetParent(acePositions[i].transform);
                newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetPosition);
                offsetPosition -= 70;
                newCard.GetComponent<CardHandler>().FlipCard(true);

                switch (i)
                {
                    case 0:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = new Color32(196, 47, 55, 255);
                        print("CUORI!");
                        break;
                    case 1:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = new Color32(196, 47, 55, 255);
                        print("QUADRI!");
                        break;
                    case 2:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;
                        print("FIORI");
                        break;
                    case 3:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;
                        print("PICCHE!");
                        break;

                }
            }
        }
    }


    //Metodo che crea il mazzo dinamicamente prima di iniziare la partita
    public void CreateDeck()
    {
        int nCards = 0;
        int offsetPosition = 0;

        //Per ogni seme creiamo 13 carte 
        for (int i = 0; i < acePositions.Length; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                //Creiamo una nuova carta dal prefab
                GameObject newCard = Instantiate(cardPrefab);

                //Aggiungiamo la carta all'array del mazzo finale che le conterrà tutte
                orderedDeck[nCards] = newCard;
                nCards++;

                //Settiamo la nuova carta come child della zona Deck
                newCard.transform.SetParent(deckPosition.transform);

                //Spostiamo la carta creata nella zona deck
                newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetPosition);

                //newCard.GetComponent<CardHandler>().FlipCard(true);

                //Assegniamo al prefab della carta le immagini e i valori corretti
                switch (i)
                {
                    case 0:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = new Color32(196, 47, 55, 255);

                        newCard.GetComponent<CardHandler>().cardType = CardSymbol.CUORI;
                        newCard.GetComponent<CardHandler>().cardColor = CardColor.ROSSO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().cardType + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("CUORI!");
                        break;
                    case 1:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = new Color32(196, 47, 55, 255);

                        newCard.GetComponent<CardHandler>().cardType = CardSymbol.QUADRI;
                        newCard.GetComponent<CardHandler>().cardColor = CardColor.ROSSO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().cardType + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("QUADRI!");
                        break;
                    case 2:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;

                        newCard.GetComponent<CardHandler>().cardType = CardSymbol.FIORI;
                        newCard.GetComponent<CardHandler>().cardColor = CardColor.NERO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().cardType + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("FIORI");
                        break;
                    case 3:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;

                        newCard.GetComponent<CardHandler>().cardType = CardSymbol.PICCHE;
                        newCard.GetComponent<CardHandler>().cardColor = CardColor.NERO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().cardType + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("PICCHE!");
                        break;

                }
            }
        }

        //Chiamiamo il metodo che mescola le carte del mazzo appena creato
        ShuffleCardsPosition();

    }


    //Metodo che mescola le carte prima di distribuirle sul campo
    public void ShuffleCardsPosition()
    {
        //Mescoliamo le carte del mazzo nell'area deck del campo da gioco
        for (int i = 0; i < orderedDeck.Length; i++)
        {
            int randomIndex = Random.Range(0, 52);
            orderedDeck[i].transform.SetSiblingIndex(randomIndex);
        }

        //Aggiungiamo le carte mescolate ad una lista di oggetti da cui poi le tireremo fuori per metterle sul campo
        int children = deckPosition.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            shuffledDeck.Add(deckPosition.transform.GetChild(i).gameObject);
        }


        for (int i = 0; i < 28; i++)
        {
            shuffledDeck[i].transform.SetParent(cardToField.transform);
            shuffledDeck[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        StartCoroutine(SetCardsOnPlayField());
    }


    //Estrae e posiziona le carte dal mazzo nel campo all'inizio della partita
    public IEnumerator SetCardsOnPlayField ()
    {
        int offsetPosition = 0;
        int cardToExtract = 0;      

        //Per ogni posizione estraiamo dal mazzo le carte da posizionare
        for (int i = 0; i < 7; i++)
        {
            //Resettiamo l'offset ad ogni nuova posizione sul campo da gioco
            offsetPosition = 0;

            //Per ogni posizione nel campo estraiamo tante carte quanto è il numero della posizione + 1
            for (int j = 0; j < (i+1); j++)
            {
 
                //Muoviamo la carta nella nuova posizione in campo
                float timeToComplete = 0.05f;

                var startPos = shuffledDeck[cardToExtract].transform.position;
                float currTime = 0f;
                remainingTimePerc = 0f;

                while (currTime < timeToComplete)
                {
                    currTime += Time.deltaTime;
                    remainingTimePerc += Time.deltaTime / timeToComplete;
                    shuffledDeck[cardToExtract].transform.position = Vector3.Lerp(startPos, fieldPositions[i].transform.position + new Vector3(0f, offsetPosition, 0f), currTime / timeToComplete);
                    yield return null;
                }

                //Se è l'ultima carta piazzata di questa posizione la scopriamo
                if (i == j)
                {
                    shuffledDeck[cardToExtract].GetComponent<CardHandler>().FlipCard(true);
                    shuffledDeck[cardToExtract].GetComponent<CardHandler>().cardStatus = CardStatus.UNCOVERED;
                }

                //Agganciamo la carta estratta come child alla posizione del campo corretta
                shuffledDeck[cardToExtract].transform.SetParent(fieldPositions[i].transform);

                //Settiamo alla carta estratta che la sua posizione è ora sul campo = FIELD
                shuffledDeck[cardToExtract].GetComponent<CardHandler>().cardPosition = CardPosition.FIELD;

                //Decrementiamo l'offset per fare in modo che ogni carta non sia completamente sovrapposta alla precedente
                offsetPosition -= 30;

                //Aumentiamo il valore così da prendere la carta successiva dal mazzo
                cardToExtract++;
            }
        }

        //Spegne l'oggetto che indicava alle carte del mazzo dove posizionarsi
        cardToField.SetActive(false);

        //Riempie due liste con le carte in campo e quelle rimaste nel mazzo
        SetCardsOnList();

        //Attiva il bottone che permette di pescare le carte dal mazzo
        drawButton.SetActive(true);

        print("Total Cards Extracted: " + cardToExtract);
    }


    //Metodo per traslare una carta da una posizione ad un'altra
    public IEnumerator MoveCardToPosition(GameObject cardToMove, Vector3 newPos, float timeToReach)
    {
        float timeToComplete = timeToReach;

        var lastPos = cardToMove.transform.position;
        float currTime = 0f;
        float remainingTimePerc = 0f;

        while (currTime < timeToComplete)
        {
            currTime += Time.deltaTime;
            remainingTimePerc += Time.deltaTime / timeToComplete;
            cardToMove.transform.position = Vector3.Lerp(lastPos, newPos, currTime / timeToComplete);
            yield return null;
        }

    }

    
    //Creiamo e riempiamo due List di carte: quelle in campo e quelle rimaste nel mazzo
    public void SetCardsOnList ()
    {
        for (int i = 0; i < shuffledDeck.Count; i++)
        {
            if(shuffledDeck[i].GetComponent<CardHandler>().cardPosition == CardPosition.FIELD)
            {
                //Aggiungiamo la carta trovata a una lista di carte che sono sul campo
                cardsOnField.Add(shuffledDeck[i]);
            } else if (shuffledDeck[i].GetComponent<CardHandler>().cardPosition == CardPosition.DECK)
            {
                //Aggiungiamo la carta trovata alla lista di carte rimaste nel mazzo
                cardsInDeck.Add(shuffledDeck[i]);
            }
        }
    }


    //Metodo che estrae le carte dal mazzo e le sposta nella zona di Draw
    public void DrawCard ()
    {
        //Aggiungiamo + 1 al contatore mosse
        GameActionHandler();

        int position = 2;

        //Se c'è almeno una carta nel mazzo la estraiamo e posizioniamo nella zona draw corretta
        if (deckPosition.transform.childCount > 0)
        {
            GameObject cardToExtract = deckPosition.transform.GetChild(deckPosition.transform.childCount - 1).gameObject;

            if(drawCardsPositions[0].transform.childCount < 1)
            {
                print("POSITION 1");
                position = 0;
            } else if (drawCardsPositions[1].transform.childCount < 1)
            {
                print("POSITION 2");
                position = 1;

                //Disabilitiamo il raycast alla carta nella posizione precedente
                GameObject cardInPos0 = drawCardsPositions[0].transform.GetChild(0).gameObject;
                cardInPos0.GetComponent<CanvasGroup>().blocksRaycasts = false;

            } else if (drawCardsPositions[2].transform.childCount < 1)
            {
                print("POSITION 3");
                position = 2;

                //Disabilitiamo il raycast alla carta nella posizione precedente
                GameObject cardInPos1 = drawCardsPositions[1].transform.GetChild(0).gameObject;
                cardInPos1.GetComponent<CanvasGroup>().blocksRaycasts = false;

            } else
            {
                //La carta in posizione 1 va in posizione 0
                GameObject cardInPos1 = drawCardsPositions[1].transform.GetChild(0).gameObject;
                cardInPos1.transform.SetParent(drawCardsPositions[0].transform);
                StartCoroutine(MoveCardToPosition(cardInPos1, drawCardsPositions[0].transform.position, 0.5f));
                cardInPos1.GetComponent<CanvasGroup>().blocksRaycasts = false;

                //La carta in posizione 2 va in posizione 1
                GameObject cardInPos2 = drawCardsPositions[2].transform.GetChild(0).gameObject;
                cardInPos2.transform.SetParent(drawCardsPositions[1].transform);
                StartCoroutine(MoveCardToPosition(cardInPos2, drawCardsPositions[1].transform.position, 0.5f));
                cardInPos2.GetComponent<CanvasGroup>().blocksRaycasts = false;

            }

            //La carta nuova va in posizione 2
            cardToExtract.GetComponent<CardHandler>().cardPosition = CardPosition.DRAW;
            cardToExtract.GetComponent<CardHandler>().cardStatus = CardStatus.UNCOVERED;
            cardToExtract.GetComponent<CardHandler>().CheckStatus();
            cardToExtract.transform.SetParent(drawCardsPositions[position].transform);
            StartCoroutine(MoveCardToPosition(cardToExtract, drawCardsPositions[position].transform.position, 0.5f));

        } else
        {
            //Sottraiamo 100 punti per aver resettato il mazzo
            ScoreHandler(-100);

            StartCoroutine(DrawCardBackInDeckCO());

        }
    }


    public IEnumerator DrawCardBackInDeckCO()
    {
        //Disabilitiamo il bottone per pescare dal mazzo 
        drawButton.GetComponent<Button>().interactable = false;

        //Rimettiamo tutte le carte nel mazzo coprendole
        for (int i = 0; i < cardsInDeck.Count; i++)
        {
            if (cardsInDeck[i].GetComponent<CardHandler>().cardPosition == CardPosition.DRAW)
            {
                //COSI' NON VA BENE!!!
                //cardsInDeck[i].GetComponent<Animator>().speed = 3;

                cardsInDeck[i].GetComponent<CardHandler>().cardPosition = CardPosition.DECK;
                cardsInDeck[i].GetComponent<CardHandler>().cardStatus = CardStatus.COVERED;
                cardsInDeck[i].GetComponent<CardHandler>().CheckStatus();
                cardsInDeck[i].transform.SetParent(deckPosition.transform);
                cardsInDeck[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                StartCoroutine(MoveCardToPosition(cardsInDeck[i], deckPosition.transform.position, 0.01f));
            }
        }

        yield return new WaitForSeconds(1f);

        //Riattiviamo il bottone per pescare dal mazzo dopo che le carte sono tornate al loro posto
        drawButton.GetComponent<Button>().interactable = true;
    }


    public void UseDrawCardConsequences ()
    {
        if(drawCardsPositions[0].transform.childCount > 1)
        {
            //Rispostiamo l'ultima carta in posizione 1 nella posizione 2 che è rimasta vuota
            GameObject cardInPos1 = drawCardsPositions[1].transform.GetChild(drawCardsPositions[1].transform.childCount - 1).gameObject;
            cardInPos1.transform.SetParent(drawCardsPositions[2].transform);
            StartCoroutine(MoveCardToPosition(cardInPos1, drawCardsPositions[2].transform.position, 0.5f));
            cardInPos1.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //Rispostiamo l'ultima carta in posizione 0 nella posizione 1
            GameObject cardInPos0 = drawCardsPositions[0].transform.GetChild(drawCardsPositions[0].transform.childCount - 1).gameObject;
            cardInPos0.transform.SetParent(drawCardsPositions[1].transform);
            StartCoroutine(MoveCardToPosition(cardInPos0, drawCardsPositions[1].transform.position, 0.5f));

        } else
        {
            //In caso non avvengano spostamenti allora riattiviamo il raycast alle carte in posizione 0 o 1 così da poterle utilizzare
            if(drawCardsPositions[1].transform.childCount > 0)
            {
                GameObject cardInPos1 = drawCardsPositions[1].transform.GetChild(drawCardsPositions[1].transform.childCount - 1).gameObject;
                cardInPos1.GetComponent<CanvasGroup>().blocksRaycasts = true;
            } else if(drawCardsPositions[0].transform.childCount > 0)
            {
                GameObject cardInPos0 = drawCardsPositions[0].transform.GetChild(drawCardsPositions[0].transform.childCount - 1).gameObject;
                cardInPos0.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }


    //Metodo per aggiornare il conteggio mosse
    public void GameActionHandler ()
    {
        gameAction++;
        gameActionText.text = gameAction.ToString();
        gameActionTextGO.text = gameAction.ToString();
    }


    //Metodo per aggiornare il conteggio punti
    public void ScoreHandler(int value)
    {
        score += value;
        if(score < 0)
        {
            score = 0;
        }
        scoreText.text = score.ToString();
        scoreTextGO.text = score.ToString();
    }


    //Gestisce di quanto aumentare o diminuire il punteggio in base all'azione compiuta: cioè provenienza e destinazione carte spostate
    public void ScoreBehaviour (CardPosition cardPosition, CardPosition cardDestination, bool doubleTap)
    {
        print("DOUBLE TAP SCORE: " + doubleTap);

        //Se si sposta una carta dalla zona FINAL alla zona FIELD
        if(cardPosition == CardPosition.FINAL)
        {
            if(cardDestination == CardPosition.FIELD)
            {
                ScoreHandler(-15);
                return;
            }
        }

        //Se si sposta una carta dalla zona DRAW alla zona FIELD o FINAL
        if (cardPosition == CardPosition.DRAW)
        {
            if (cardDestination == CardPosition.FIELD )
            {
                if (doubleTap)
                {
                    ScoreHandler(10);
                } else
                {
                    ScoreHandler(5);
                }
                return;
            } else if(cardDestination == CardPosition.FINAL)
            {
                ScoreHandler(10);
                return;
            }
        }

        //Se si sposta una carta dalla zona FIELD alla zona FINAL
        if (cardPosition == CardPosition.FIELD)
        {
            if (cardDestination == CardPosition.FINAL)
            {
                ScoreHandler(10);
                return;
            }
        }

        print("NESSUN PUNTEGGIO PER QUESTA AZIONE");
    }



    //Controlla se ogni carta scoperta in campo può agganciarsi a quelle della zona finale e in tal caso le sposta tutte e completa la partita
    //Viene chiamato solo dopo che tutte le carte sono in campo e scoperta
    public void CheckAutomaticVictory()
    {
        if (!stopCheck)
        {
            int controlValue = 0;

            if (deckPosition.childCount == 0)
            {
                controlValue++;
            }

            for (int i = 0; i < orderedDeck.Length; i++)
            {
                if (orderedDeck[i].GetComponent<CardHandler>().cardStatus.Equals(CardStatus.COVERED))
                {
                    return;
                }
            }

            controlValue++;

            if (controlValue > 1)
            {
                stopCheck = true;
                StartCoroutine(AutomaticVictory());
            }
        }
    }


    //Esegue il posizionamento automatico di tutte le carte rimaste in campo
    public IEnumerator AutomaticVictory ()
    {
        while(!isVictory)
        {
            for (int i = 0; i < orderedDeck.Length; i++)
            {
                if (orderedDeck[i].GetComponent<CardHandler>().cardPosition.Equals(CardPosition.FIELD))
                {
                    if(orderedDeck[i].transform.childCount < 5)
                    {
                        orderedDeck[i].GetComponent<CardInteraction>().ExecuteCheckOnAllCardInField();
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }
    }


    //Controlla se tutte le carte sono nella zona vittoria in tal caso attiva il panel game over e chiude la partita
    public void CheckSituationForVictory()
    {
        int setCompleted = 0;

        for (int i = 0; i < acePositions.Length; i++)
        {
            if (acePositions[i].transform.childCount == 14)
            {
                setCompleted++;
            }
        }

        if(setCompleted == 4)
        {
            isVictory = true;
            gameOverPanel.SetActive(true);
        }
    }


    //Cicliamo sulla struttura dati e riportiamo tutte le carte alla loro situazione precedente e anche il punteggio
    public void UndoAction ()
    {
        GameActionHandler();




    }


}
