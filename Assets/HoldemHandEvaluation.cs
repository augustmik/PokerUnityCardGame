using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class HoldemHandEvaluation : IHoldemHandEvaluation
    {
        int flushSuit = -1;
        int topCard = 99;
        public int EvaluateHand(LinkedList<Card> Hand, LinkedList<Card> BoardCards)
        {
            int handRank = 10;
            
            int pairCount = CountPairs(Hand, BoardCards);
            if (pairCount == 1) { handRank = 9; }
            if (pairCount == 2) { handRank = 8; }
            if (IsTrips(Hand, BoardCards)) { handRank = 7;}
            if (IsStraight(Hand, BoardCards)) { handRank = 6; }
            if (IsFlush(Hand, BoardCards)) { handRank = 5; }
            if (IsTrips(Hand, BoardCards) && pairCount >= 2) { handRank = 4; } //FullHouse
            if (IsQuads(Hand, BoardCards)) { handRank = 3; }  
            if (IsStrFlush(Hand, BoardCards)) { handRank = 2; }
            if (handRank==2 && topCard == 0) { handRank = 1; }


            return handRank;
        }
        bool IsStrFlush(LinkedList<Card> Hand, LinkedList<Card> BoardCards) //is there a straightFlush
        {
            if (IsFlush(Hand, BoardCards))
            {
                int[] allCards = new int[7];
                int j = -1, cardsInRow=1, reachedCount=0, a = 0;
                allCards = SortCards(Hand, BoardCards);
                int[] flushCards = new int[7];
                var currentNode = Hand.First;
                //Debug.Log("flush suit: "+flushSuit);
                while (currentNode != null)
                {
                    if ((int)currentNode.Value.Type == flushSuit)
                    {
                        flushCards[a] = LowerCardValueNumber(currentNode.Value.CardNumber);
                        a++;
                    }
                    //Debug.Log((int)currentNode.Value.Type);
                    currentNode = currentNode.Next;
                }
                currentNode = BoardCards.First;
                //Debug.Log(flushSuit);
                while (currentNode != null)
                {
                    if ((int)currentNode.Value.Type == flushSuit)
                    {
                        flushCards[a] = LowerCardValueNumber(currentNode.Value.CardNumber);
                        a++;
                    }
                    //Debug.Log((int)currentNode.Value.Type);
                    currentNode = currentNode.Next;
                }

                flushCards = SortCards(flushCards);

                for (int i=0; i<flushCards.Length-1; i++)
                {
                    if (flushCards[i] == (flushCards[i + 1] + 1)) {
                        cardsInRow++;
                        reachedCount = cardsInRow;
                    }
                    else if (flushCards[i] != flushCards[i + 1]) { cardsInRow = 1;}
                }
                //Debug.Log("reached count: "+reachedCount);
                if (reachedCount >= 5) {  return true; }
                else if (reachedCount == 4 && allCards[0] == 12 && allCards[6] == 0) { Debug.Log("Royal"); topCard = 0; return true; }
                else return false;
            }
            return false;
        }
        bool IsQuads(LinkedList<Card> Hand, LinkedList<Card> BoardCards) //is there four of a kind
        {
            int[] allCards = new int[7];
            allCards = SortCards(Hand, BoardCards);
            for (int i = 0; i < 7 - 3; i++)
            {
                if (allCards[i] == allCards[i + 1] && allCards[i] == allCards[i + 2] && allCards[i] == allCards[i+3]) { return true; }

            }
            return false;
        }
        bool IsStraight(LinkedList<Card> Hand, LinkedList<Card> BoardCards) //is there a straight
        {
            int[] allCards = new int[7];
            int cardsInRow = 1, reachedCount = 0;
            allCards = SortCards(Hand, BoardCards);
            for (int i=0; i<7-1; i++)
            {
                //Debug.Log(allCards[i]);
                if (allCards[i] == (allCards[i + 1] + 1)) { cardsInRow++; reachedCount = cardsInRow; }
                else if (allCards[i] != allCards[i + 1]) { cardsInRow = 1; }
            }
            //Debug.Log("reached: " + reachedCount);
            if (reachedCount >= 5) { return true; }
            else if (reachedCount == 4 && allCards[0] == 12 && allCards[6]==0) { return true; }
            else return false;
        }
        bool IsTrips(LinkedList<Card> Hand, LinkedList<Card> BoardCards) //is there a trips
        {
            int[] allCards = new int[7];            
            allCards = SortCards(Hand, BoardCards);
            for (int i = 0; i < 7 - 2; i++)
            {
                if (allCards[i] == allCards[i + 1] && allCards[i] == allCards[i + 2]) { return true; }

            }
            return false;
        }
        bool IsFlush(LinkedList<Card> Hand, LinkedList<Card> BoardCards) //is there a Flush
        {
            int DNumber = 0, SNumber = 0, CNumber = 0, HNumber = 0;
            var currentNode = Hand.First;
            while (currentNode != null)
            {
                if (0 == (int)currentNode.Value.Type) { HNumber++; }
                if (1 == (int)currentNode.Value.Type) { SNumber++; }
                if (2 == (int)currentNode.Value.Type) { DNumber++; }
                if (3 == (int)currentNode.Value.Type) { CNumber++; }
                currentNode = currentNode.Next;
            }
            var currentoNode = BoardCards.First;
            while (currentoNode != null)
            {
                if (0 == (int)currentoNode.Value.Type) { HNumber++; }
                if (1 == (int)currentoNode.Value.Type) { SNumber++; }
                if (2 == (int)currentoNode.Value.Type) { DNumber++; }
                if (3 == (int)currentoNode.Value.Type) { CNumber++; }
                currentoNode = currentoNode.Next;
            }
            //Debug.Log("HNumber: "+ HNumber + ", SNumber: " + SNumber + ", DNumber: " + DNumber + ", CNumber: " + CNumber);
            if (HNumber >= 5) { flushSuit = 0; }
            if (SNumber >= 5) { flushSuit = 1; }
            if (DNumber >= 5) { flushSuit = 2; }
            if (CNumber >= 5) { flushSuit = 3; }
            if (DNumber >= 5 || HNumber >= 5 || SNumber >= 5 || CNumber >= 5) { return true; }
            return false;
        }
        int CountPairs(LinkedList<Card> Hand, LinkedList<Card> boardCards) //count all pairs for one pair, two pair and fullhouses
        {
            int[] allCards = new int[7];
            int pairs = 0;
            int remember = -1;
            allCards = SortCards(Hand, boardCards);
            for (int i=0; i < 7-1; i++)
            {
                if (allCards[i] == allCards[i + 1] && remember != allCards[i+1]) { remember = allCards[i]; pairs++; }
                
            }
            //Debug.Log("Pair Count: "+pairs);
            return pairs;
        }
        int[] SortCards(LinkedList<Card> heldCards, LinkedList<Card> boardCards) //sorts the ccards according to their value
        {
            int[] sortedCards = new int[7];
            var currentNode = boardCards.First;
            sortedCards[0] = LowerCardValueNumber(heldCards.First.Value.CardNumber);
            sortedCards[1] = LowerCardValueNumber(heldCards.First.Next.Value.CardNumber);
            for (int i=0+2; i<5+2; i++)
            {
                sortedCards[i] = LowerCardValueNumber(currentNode.Value.CardNumber);
                currentNode = currentNode.Next;
            }
            int change = 0;
            for (int i = 0; i < 7; i++){
                for (int j = i+1; j < 7; j++){
                    if (sortedCards[i] < sortedCards[j])
                        {
                        change = sortedCards[i];
                        sortedCards[i] = sortedCards[j];
                        sortedCards[j] = change;
                        }
                }
            }

            return sortedCards;
        }
        int[] SortCards(int[] cards) //sorts the cards according to their value, but not 
        {
            int change;
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = i + 1; j < cards.Length-1; j++)
                {
                    if (cards[i] < cards[j])
                    {
                        change = cards[i];
                        cards[i] = cards[j];
                        cards[j] = change;
                    }
                }
            }

            return cards;
        }
        int LowerCardValueNumber (int cardValue) // lowers the spritesheet card values (both ace of spades and clubs would represent the same value)
        {
            if (cardValue <= 12) { return cardValue; }
            if (cardValue > 12 && cardValue <= 25) { return cardValue - 13; }
            if (cardValue > 25 && cardValue <= 38) { return cardValue - 26; }
            else return cardValue - 39;
        }
    }
}
