using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicTable : MonoBehaviour
{
    private List<GameObject> _cards;
    private Deck _deck;

    void AddformDeck()
    {
        CardScript newcard = new CardScript;
        newcard.Initalize(_deck.Draw());
        _cards.Add();
    }
}
