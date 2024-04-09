using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int handSize;
    private int currentMana;
    [SerializeField] private int maxMana;
    [SerializeField] private TextMeshProUGUI manaText;

    [SerializeField] private GameObject hand;

    [SerializeField] private List<GameObject> cards;

    private Queue<GameObject> deck = new Queue<GameObject>();
    private Queue<GameObject> cardsInHand = new Queue<GameObject>();
    private Stack<GameObject> discard = new Stack<GameObject>();

    [SerializeField] private TextMeshProUGUI deckSize;
    [SerializeField] private TextMeshProUGUI discardSize;

    private System.Random random = new System.Random();

    void Start()
    {

        foreach (var card in cards)
        {
            deck.Enqueue(card);
        }

        deckSize.text = $"{deck.Count}";
        discardSize.text = $"{discard.Count}";
        StartCoroutine(FirstDraw());
    }



    // Update is called once per frame
    void Update()
    {
        UseCard();
    }

    private void UseCard()
    {
        if (Input.GetKeyDown(KeyCode.G) && cardsInHand.Count > 0 && currentMana - cardsInHand.Peek().GetComponent<Card>().manaCost >= 0)
        {
            currentMana -= cardsInHand.Peek().GetComponent<Card>().manaCost;
            manaText.text = $"{currentMana}/{maxMana}";
            discard.Push(cardsInHand.Peek());
            discardSize.text = $"{discard.Count}";
            Destroy(cardsInHand.Dequeue());
        }
    }

    private void GenerateTurn()
    {
        currentMana = maxMana;

        manaText.text = $"{currentMana}/{maxMana}";

        for (int i = 0; i < handSize; i++)
        {
            cardsInHand.Enqueue(Instantiate(deck.Dequeue(), hand.transform));
            deckSize.text = $"{deck.Count}";
        }
    }

    public void EndTurn()
    {
        int queueCount = cardsInHand.Count;

        for (int i = 0; i < queueCount; i++)
        {
            discard.Push(cardsInHand.Peek());
            Destroy(cardsInHand.Dequeue());
        }
        discardSize.text = $"{discard.Count}";

        if (deck.Count == 0)
        {
            int discardCount = discard.Count;

            FisherYatesShuffle(ref cards);

            for (int i = 0; i < discardCount; i++)
            {
                deck.Enqueue(cards[i]);
                discard.Pop();
            }

            discardSize.text = $"{discard.Count}";
            deckSize.text = $"{deck.Count}";
        }
        GenerateTurn();
    }

    private IEnumerator FirstDraw()
    {
        yield return new WaitForSeconds(1);
        GenerateTurn();
    }

    private void FisherYatesShuffle(ref List<GameObject> List)
    {
        int n = List.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);

            var value = List[k];
            List[k] = List[n];
            List[n] = value;
        }
    }
}
