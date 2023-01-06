using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<CardData> cards;
    public bool doShuffle;
    // Start is called before the first frame update
    void Start()
    {
        LoadCards();
        if (doShuffle)
            Shuffle();
    }

    void LoadCards()
    {
        StreamReader fs = new StreamReader(Path.Combine(Application.dataPath, "Resources/Json/cards.json"));
        this.cards = JsonUtility.FromJson<CardLoadData>(fs.ReadToEnd()).cards;
    }

    void Shuffle()
    {
        for(int i=0; i<cards.Count - 1; i++) {
            int rnd = Random.Range(i, cards.Count - 1);
            CardData tmp = cards[i];
            cards[i] = cards[rnd];
            cards[rnd] = tmp;
        }
    }
    public CardData Draw()
    {
        CardData ret = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return ret;
    }
}

public class CardLoadData
{
    public List<CardData> cards;
}