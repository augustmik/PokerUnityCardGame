using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Cards // : MonoBehaviour
{
    Sprite[] cardFaces;
    Sprite cardBack;
    GameObject card;
    SpriteRenderer spriteRend;
    //int[] usedCards;
    //bool action = false;
    public Cards()
    {
        cardFaces = Resources.LoadAll<Sprite>("PlayingCards");
    }
    void Awake()
    {
        //cardFaces = Resources.LoadAll<Sprite>("PlayingCards");
        //Debug.Log("Sprites loaded: " + cardFaces.Length);
        //usedCards = new int[9];
    }
    public void LoadPlayerCards(int rand1, int rand2) //displays players cards
    {
        card = GameObject.Find("Card1");
        spriteRend = card.GetComponent<SpriteRenderer>();  
        spriteRend.sprite = cardFaces[rand1];
        card = GameObject.Find("Card2");
        spriteRend = card.GetComponent<SpriteRenderer>();
        spriteRend.sprite = cardFaces[rand2];
    }
    public void LoadVillainCards(int rand1, int rand2) // displays villains cards
    {
        card = GameObject.Find("VillainCard1");
        spriteRend = card.GetComponent<SpriteRenderer>();
        spriteRend.sprite = cardFaces[rand1];
        card = GameObject.Find("VillainCard2");
        spriteRend = card.GetComponent<SpriteRenderer>();
        spriteRend.sprite = cardFaces[rand2];
    }
    public void LoadCard(int cardNr, int cardRand) //displays board cards
    {
        StringBuilder cardStr = new StringBuilder();
        cardStr.Append("BoardCard" + cardNr);
        //Debug.Log(cardStr.ToString());
        card = GameObject.Find(cardStr.ToString());
        //Debug.Log(card.name);
        spriteRend = card.GetComponent<SpriteRenderer>();
        //Debug.Log(spriteRend);
        spriteRend.sprite = cardFaces[cardRand];
        //Debug.Log("Generating " + cardNr + "with Nr "+ cardRand);
    }

}
