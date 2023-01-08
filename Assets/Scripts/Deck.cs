using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
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
    }

    void LoadCards()
    {
        //StreamReader fs = new StreamReader(Path.Combine(Application.dataPath, "Resources/Json/cards.json"));
        StreamReader fs = new StreamReader(Path.Combine(Application.streamingAssetsPath ,"cards_.json"));
        string str = fs.ReadToEnd();
        var json = JsonUtility.FromJson<CardLoadData>(str);
        _cards = new List<CardData>();
        foreach (var card in json.cards)
        {
            CardData newCard = new CardData();
            newCard.CardNum = card.id;
            newCard.Price = card.price;
            newCard.Effect = card.effect;
            newCard.Turn = card.turn;
            newCard.Slot = card.slot;

            _cards.Add(newCard);
        }
    }

    void Shuffle()
    {
        for(int i=0; i<_cards.Count - 1 - 4; i++) {
            int rnd = UnityEngine.Random.Range(i, _cards.Count - 4);
            exchange(i, rnd);
        }
        for(int i=1;i<=4;i++)
        {
            int rnd = UnityEngine.Random.Range((_cards.Count - 4)/3, _cards.Count - 4 + i);
            exchange(_cards.Count - 1 - 4 + i, rnd);
        }
    }
    private void exchange(int x, int y)
    {
        CardData tmp = _cards[x];
        _cards[x] = _cards[y];
        _cards[y] = tmp;
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
    public List<CardFromJson> cards;
}

[System.Serializable]
public class CardFromJson
{
    public int id;
    public List<int> price;
    public List<int> effect;
    public int turn;
    public int slot;
}