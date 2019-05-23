using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuit : byte
{
    Hearts = 0,
    Spades = 1,
    Diamonds = 2,
    Clubs = 3,
}
public class Card
{
    public CardSuit Type { get; private set; }
    public int CardNumber { get; private set; }

    public Card(int number, CardSuit type)
    {
        CardNumber = number;
        Type = type;
    }
}
