﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject cardPrefab;

    public Transform deckPosition;

    public GameObject[] fieldPositions;

    public GameObject[] acePositions;

    public GameObject[] drawCardsPositions;

    public Sprite[] cardValues;

    public Sprite[] cardSemi;

    //public Sprite semeCuori, semeQuadri, semeFiori, SemePicche;
    public Sprite fondoCarta;
    public GameObject cardToField;

    public GameObject[] orderedDeck = new GameObject[52];

    public List<GameObject> shuffledDeck;


    bool exitToCoro;
    float remainingTimePerc;

    // Use this for initialization
    void Start()
    {
        orderedDeck = new GameObject[52];
        CreateDeck();
    }

    // Update is called once per frame
    void Update()
    {

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

                        newCard.GetComponent<CardHandler>().segno = "CUORI";
                        newCard.GetComponent<CardHandler>().typeOfCard = CardSymbol.CUORI;
                        newCard.GetComponent<CardHandler>().colorOfCard = CardColor.ROSSO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().segno + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("CUORI!");
                        break;
                    case 1:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = new Color32(196, 47, 55, 255);

                        newCard.GetComponent<CardHandler>().segno = "QUADRI";
                        newCard.GetComponent<CardHandler>().typeOfCard = CardSymbol.QUADRI;
                        newCard.GetComponent<CardHandler>().colorOfCard = CardColor.ROSSO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().segno + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("QUADRI!");
                        break;
                    case 2:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;

                        newCard.GetComponent<CardHandler>().segno = "FIORI";
                        newCard.GetComponent<CardHandler>().typeOfCard = CardSymbol.FIORI;
                        newCard.GetComponent<CardHandler>().colorOfCard = CardColor.NERO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().segno + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("FIORI");
                        break;
                    case 3:
                        newCard.transform.GetChild(0).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(1).GetComponent<Image>().sprite = cardSemi[i];
                        newCard.transform.GetChild(2).GetComponent<Image>().sprite = cardValues[j];
                        newCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;

                        newCard.GetComponent<CardHandler>().segno = "PICCHE";
                        newCard.GetComponent<CardHandler>().typeOfCard = CardSymbol.PICCHE;
                        newCard.GetComponent<CardHandler>().colorOfCard = CardColor.NERO;

                        newCard.GetComponent<CardHandler>().cardValue = (j + 1);
                        newCard.name = (newCard.GetComponent<CardHandler>().segno + "_" + newCard.GetComponent<CardHandler>().cardValue);

                        print("PICCHE!");
                        break;

                }
            }
        }

        //Chiamiamo il metodo che mescola le carte del mazzo appena creato
        ShuffleCardsPosition();

    }


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


    //Trasformare questo metodo in coroutine e temporizzare le transizioni delle carte dal deck alla nuova posizione sul campo <------
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

                shuffledDeck[cardToExtract].transform.SetParent(fieldPositions[i].transform);

                //StartCoroutine(MoveToPosition(shuffledDeck[cardToExtract], fieldPositions[i], 1f, offsetPosition));


                //shuffledDeck[cardToExtract].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetPosition);

                offsetPosition -= 30;

                //Se è l'ultima carta piazzata di questa posizione la scopriamo
                if(i == j)
                {
                    shuffledDeck[cardToExtract].GetComponent<CardHandler>().FlipCard(true);
                }

                //Aumentiamo il valore così da prendere la carta successiva dal mazzo
                cardToExtract++;
            }
        }

        print("Total Cards Extracted: " + cardToExtract);

    }

}
