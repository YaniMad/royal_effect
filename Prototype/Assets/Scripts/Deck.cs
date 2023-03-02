using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck 
{
	public List<CardData> cards;
    public List<CardData> hand;
    public CardData nextCard;

    public List<CardData> Cards
    {
        get { return cards; }
    }
    public List<CardData> Hand
    {
        get { return hand; }
    }
    public CardData NextCard
	{
		get { return nextCard; }
        set { nextCard = value; }
	}

    public CardData DrawCard()
    {
        CardData _card = cards[Random.Range(0, cards.Count - 1)]; ;
        hand.Add(_card);
        cards.Remove(_card);
        return _card;
    }

    public void RemoveHand(CardData _card)
    {
        hand.Remove(_card);
        cards.Add(_card);
    }

}
