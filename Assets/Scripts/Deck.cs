using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<CardData> _cards;
    public bool doShuffle;
    // Start is called before the first frame update
    void Awake()
    {
        LoadCards();
        if (doShuffle)
            Shuffle();
        //Debug.Log(cards.Count);
    }

    void LoadCards()
    {
        StreamReader fs = new StreamReader(Path.Combine(Application.dataPath, "Resources/Json/cards.json"));
        this._cards = JsonUtility.FromJson<CardLoadData>(fs.ReadToEnd()).cards;
    }

    void Shuffle()
    {
        for(int i=0; i<_cards.Count - 1; i++) {
            int rnd = Random.Range(i, _cards.Count - 1);
            CardData tmp = _cards[i];
            _cards[i] = _cards[rnd];
            _cards[rnd] = tmp;
        }
    }
    public CardData Draw()
    {
        CardData ret = _cards[_cards.Count - 1];
        _cards.RemoveAt(_cards.Count - 1);
        return ret;
    }

    public List<CardData> cards { get { return _cards; } }
}

public class CardLoadData
{
    public List<CardData> cards;
}