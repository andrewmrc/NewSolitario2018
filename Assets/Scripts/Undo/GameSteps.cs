using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSteps {

    public string stepIndex;
    public int scoreRecorded;
    public List<CardData> cardDataRecorded = new List<CardData>();

}
