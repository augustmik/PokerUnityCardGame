using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    int ChipStack { get; set;}
    LinkedList<Card> CardsL;
    int HandStrength { get; set; }

    public Player()
    {
        CardsL = new LinkedList<Card>();
    }
    public void GiveCard(Card card)
    {
        CardsL.AddLast(card);
    }
    public void ClearHand()
    {
        CardsL.Clear();
    }
    public LinkedList<Card> GetHand()
    {
        return CardsL;
    }
    public void PostBlind(int amount)
    {
        ChipStack = ChipStack - amount;
    }

    public abstract void Bet();
    public abstract void Check();
    public abstract void Raise();
    public abstract void Fold();


}
