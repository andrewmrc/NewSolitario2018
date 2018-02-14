using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData {

    public string cardName;
    public Transform actualParent;
    public Vector3 actualTransform;
    public CardStatus actualCardStatus;
    public CardPosition actualCardPosition;
    public bool raycast;

}
