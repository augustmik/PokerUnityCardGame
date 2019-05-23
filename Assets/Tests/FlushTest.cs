using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets;

namespace Tests
{
    public class FlushTest
    {
        IHoldemHandEvaluation ievaluateHands = new HoldemHandEvaluation();
        [Test]
        public void FlushTestSimplePasses()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(14, CardSuit.Hearts));
            hand.AddLast(new Card(25, CardSuit.Hearts));

            board.AddLast(new Card(15, CardSuit.Hearts));
            board.AddLast(new Card(6, CardSuit.Clubs));
            board.AddLast(new Card(18, CardSuit.Hearts));
            board.AddLast(new Card(45, CardSuit.Diamonds));
            board.AddLast(new Card(23, CardSuit.Hearts));

            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 5);
        }
        [Test]
        public void RoyalTest()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(13, CardSuit.Hearts));
            hand.AddLast(new Card(22, CardSuit.Hearts));

            board.AddLast(new Card(24, CardSuit.Hearts));
            board.AddLast(new Card(6, CardSuit.Clubs));
            //board.AddLast(new Card(18, CardSuit.Hearts));
            board.AddLast(new Card(7, CardSuit.Clubs));
            board.AddLast(new Card(25, CardSuit.Hearts));
            board.AddLast(new Card(23, CardSuit.Hearts));

            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 1);
        }
        [Test]
        public void FullHouseTest()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(0, CardSuit.Clubs));
            hand.AddLast(new Card(13, CardSuit.Hearts));

            board.AddLast(new Card(14, CardSuit.Hearts));
            board.AddLast(new Card(26, CardSuit.Spades));
            board.AddLast(new Card(27, CardSuit.Spades));
            board.AddLast(new Card(16, CardSuit.Hearts));
            board.AddLast(new Card(29, CardSuit.Spades));

            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 4);
        }
        [Test]
        public void TripsTest()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(0, CardSuit.Clubs));
            hand.AddLast(new Card(13, CardSuit.Hearts));

            board.AddLast(new Card(25, CardSuit.Hearts));
            board.AddLast(new Card(26, CardSuit.Spades));
            board.AddLast(new Card(18, CardSuit.Hearts));
            board.AddLast(new Card(45, CardSuit.Diamonds));
            board.AddLast(new Card(23, CardSuit.Hearts));

            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 7);
        }
        [Test]
        public void QuadsTest()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(0, CardSuit.Clubs));
            hand.AddLast(new Card(13, CardSuit.Hearts));

            board.AddLast(new Card(25, CardSuit.Hearts));
            board.AddLast(new Card(26, CardSuit.Spades));
            board.AddLast(new Card(18, CardSuit.Hearts));
            board.AddLast(new Card(39, CardSuit.Diamonds));
            board.AddLast(new Card(23, CardSuit.Hearts));

            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 3);
        }
        [Test]
        public void SimpleStraightTest()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(0, CardSuit.Clubs));
            hand.AddLast(new Card(1, CardSuit.Clubs));

            board.AddLast(new Card(15, CardSuit.Hearts));
            board.AddLast(new Card(16, CardSuit.Hearts));
            board.AddLast(new Card(43, CardSuit.Diamonds));
            board.AddLast(new Card(45, CardSuit.Diamonds));
            board.AddLast(new Card(23, CardSuit.Hearts));
            Debug.Log(ievaluateHands.EvaluateHand(hand, board));
            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 6);
        }
        [Test]
        public void AceLastStraightTest()
        {
            LinkedList<Card> hand = new LinkedList<Card>();
            LinkedList<Card> board = new LinkedList<Card>();

            hand.AddLast(new Card(13, CardSuit.Hearts));
            hand.AddLast(new Card(12, CardSuit.Clubs));

            board.AddLast(new Card(24, CardSuit.Hearts));
            board.AddLast(new Card(36, CardSuit.Spades));
            board.AddLast(new Card(43, CardSuit.Diamonds));
            board.AddLast(new Card(35, CardSuit.Spades));
            board.AddLast(new Card(23, CardSuit.Hearts));

            Assert.That(ievaluateHands.EvaluateHand(hand, board) == 6);
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        /*[UnityTest]
        public IEnumerator FlushTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }*/
    }
}
