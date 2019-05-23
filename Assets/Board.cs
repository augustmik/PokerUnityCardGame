using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public int bBlind = 2, sBlind = 1;
    Button newGameButton;
    GameObject button;
    GameObject userButtons, winnerPanel; //panels that display information
    Text winnerText;
    Cards PlayerI;
    Player[] players;
    IHoldemHandEvaluation holdemHandEvaluation = new HoldemHandEvaluation();
    LinkedList<Card> boardCards;
    int[] usedCards;
    int rand1, rand2, rand3, rand4;
    int gameState = -1;
    bool action = false;
    bool buttonOnH = true;
    void Start()
    {
        userButtons.SetActive(false);

        //winnerPanel.SetActive(false);
        newGameButton.onClick.AddListener(NewHand);
        //NewHand();
        //Flop();
    }

    // Update is called once per frame
    void Update()
    {

        /*switch (gameState)
        {
            case 0:
                NewHand();
                break;
            case 1:
                Flop();
                break;
            case 2:
                Turn();
                break;
            case 3:
                River();
                break;
            case 4:
                ShowDown();
                break;
            default:
            //user input here
                break;
        }*/
    }
    void Awake()
    {
        button = GameObject.Find("PokerButton");
        userButtons = GameObject.Find("UserButtonsPanel");
        winnerPanel = GameObject.Find("ShowdownInfoPanel");
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();



    }
    public void MoveButton() //moves the button after every hand
    {
        if (button.transform.position == new Vector3(-1246.9f, -12.779f))
        {
            button.transform.position = new Vector3(-1246.9f, 115.1f);
            buttonOnH = false;
        }
        else
        {
            button.transform.position = new Vector3(-1246.9f, -12.779f);
            buttonOnH = true;
        }
        
    }
    public void NewHand() //new hand is dealt
    {
        players = new Player[2];
        PlayerI = new Cards();
        usedCards = new int[9];
        players[0] = new HeroPlayer();
        players[1] = new VillainPlayer();
        boardCards = new LinkedList<Card>();
        Debug.Log("Nuejo iki newHand");
        winnerPanel.SetActive(false);
        rand1 = Random.Range(0, 51);


        usedCards[0] = rand1;
        rand2 = rand1;
        rand3 = rand2;
        rand4 = rand3;
        
        while (usedCards.Contains(rand2) == true)     //cards are generated randomly
        {
            rand2 = Random.Range(0, 51);
        }
        
        usedCards[1] = rand2;
        while (usedCards.Contains(rand3) == true)
        {
            rand3 = Random.Range(0, 51);
        }
        usedCards[2] = rand3;

        while (usedCards.Contains(rand4) == true)
        {
            rand4 = Random.Range(0, 51);
        }
        usedCards[3] = rand4;


        if (buttonOnH == true) {
            DealCards(0);
            PlayerI.LoadPlayerCards(rand1, rand3);
        }
        else {
            DealCards(1);
            PlayerI.LoadPlayerCards(rand2, rand4);
        }
        PostBlinds();
        PreFlop();
        //gameState = 1;
    }
    public void DealCards( int startFrom) //cards are dealt (sent) to the correct objects
    {

        if (startFrom == 0)
        {
            players[startFrom].GiveCard(new Card(rand1, DetermineSuit(rand1)));
            players[startFrom + 1].GiveCard(new Card(rand2, DetermineSuit(rand2)));
            players[startFrom].GiveCard(new Card(rand3, DetermineSuit(rand3)));
            players[startFrom + 1].GiveCard(new Card(rand4, DetermineSuit(rand4)));
        }
        else
        {
            players[startFrom].GiveCard(new Card(rand1, DetermineSuit(rand1)));
            players[startFrom - 1].GiveCard(new Card(rand2, DetermineSuit(rand2)));
            players[startFrom].GiveCard(new Card(rand3, DetermineSuit(rand3)));
            players[startFrom - 1].GiveCard(new Card(rand4, DetermineSuit(rand4)));
        }

    }
    public void PostBlinds()  //preflop blind deduction
    {
        //player[0] is Hero
        if (buttonOnH == true) { players[1].PostBlind(bBlind); players[0].PostBlind(sBlind); }
        else { players[0].PostBlind(bBlind); players[1].PostBlind(sBlind); }
    }
    CardSuit DetermineSuit(int cardNumber)   //Hand suit determination after dealing them (suits: Spades, Diamonds, Clubs & Hearts)
    {
        if (cardNumber <= 12) { return CardSuit.Clubs; }
        if (cardNumber > 12 && cardNumber <= 25) { return CardSuit.Hearts; }
        if (cardNumber <= 25 && cardNumber <= 38) { return CardSuit.Spades; }
        if (cardNumber > 38) { return CardSuit.Diamonds; }

        return 0;
    }
    public void PreFlop()  //action that happens Preflop
    {
        userButtons.SetActive(true);
        //Debug.Log("Preflop Reached");
        Flop();
    }
    public void Flop() // action that happens after flop has been dealt
    {
        while (usedCards.Contains(rand1) == true)
        {
            rand1 = Random.Range(0, 51);
        }
        usedCards[4] = rand1;
        boardCards.AddLast(new Card(rand1, DetermineSuit(rand1)));
        PlayerI.LoadCard(1,rand1);
        while (usedCards.Contains(rand2) == true)
        {
            rand2 = Random.Range(0, 51);
        }
        usedCards[5] = rand2;
        PlayerI.LoadCard(2, rand2);
        boardCards.AddLast(new Card(rand2, DetermineSuit(rand2)));

        while (usedCards.Contains(rand3) == true)
        {
            rand3 = Random.Range(0, 51);
        }
        usedCards[6] = rand3;
        PlayerI.LoadCard(3, rand3);
        boardCards.AddLast(new Card(rand3, DetermineSuit(rand3)));

        //gameState = 2;
        Turn();


    }
    public void Turn() //action that happens after turn card has been dealt
    {
        while (usedCards.Contains(rand1) == true)
        {
            rand1 = Random.Range(0, 51);
        }
        usedCards[7] = rand1;
        PlayerI.LoadCard(4, rand1);
        boardCards.AddLast(new Card(rand1, DetermineSuit(rand1)));

        //gameState = 3;
        River();
    }
    public void River() //action that happens after river card has been dealt
    {
        while (usedCards.Contains(rand1) == true)
        {
            rand1 = Random.Range(0, 51);
        }
        usedCards[8] = rand1;
        PlayerI.LoadCard(5, rand1);
        boardCards.AddLast(new Card(rand1, DetermineSuit(rand1)));

        //gameState = 4;

        ShowDown();
    }
    public void ShowDown() // Showdown, hands are evaluated and the winner is found
    {
        //Debug.Log("Showdown Reached");
        StringBuilder stringB = new StringBuilder();
        winnerPanel.SetActive(true);
        int Hstr = holdemHandEvaluation.EvaluateHand(players[0].GetHand(), boardCards);
        stringB.Append("Hero Hand Str: " + Hstr + "\n");
        Debug.Log("Hero Hand Str: "+ Hstr);
        PlayerI.LoadVillainCards(players[1].GetHand().First.Value.CardNumber, players[1].GetHand().First.Next.Value.CardNumber);
        int Vstr = holdemHandEvaluation.EvaluateHand(players[1].GetHand(), boardCards);
        stringB.Append("Villain Hand Str: " + Vstr + "\n");

        Debug.Log("Villain Hand Str: " + Vstr);
        if (Hstr < Vstr) {
            stringB.Append("Player wins!");

            Debug.Log("Player wins!");
        }
        else if (Vstr > Hstr) { stringB.Append("Computer wins"); Debug.Log("Computer wins"); }
        else if (Vstr == Hstr) { stringB.Append("Both players have the same hand strength");  Debug.Log("Both players have the same hand strength"); }

        winnerText.text = stringB.ToString();

    }
}
