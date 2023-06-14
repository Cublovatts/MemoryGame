using System.Collections.Generic;
using UnityEngine;

public class MatchSetupManager : MonoBehaviour
{
    private readonly string POSSIBLE_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [SerializeField]
    private GameObject _cardPrefab;
    [SerializeField]
    private int _numberOfCards;

    private float _currentOffset = 0f;
    private float _offset = 3f;

    void Start()
    {
        // Check for even number of cards
        if (_numberOfCards % 2 != 0)
        {
            _numberOfCards++;
        }

        List<Card> uninitialisedCards = new List<Card>();

        for (int i=0; i<_numberOfCards; i++)
        {
            GameObject newCard = Instantiate(_cardPrefab, gameObject.transform);
            newCard.transform.Translate(new Vector3(_currentOffset, 0, 0));
            _currentOffset += _offset;
            uninitialisedCards.Add(newCard.GetComponent<Card>());
        }

        for (int i=0; i<_numberOfCards/2; i++)
        {
            int initialisedCards = 0;
            char cardValue = POSSIBLE_CHARS[Random.Range(0, 25)];
            while (initialisedCards < 2)
            {
                int randomCard = Random.Range(0, _numberOfCards);
                Card currentCard = uninitialisedCards[randomCard];
                if (currentCard.FaceValue == "")
                {
                    currentCard.FaceValue = cardValue.ToString();
                    initialisedCards++;
                }
            }
        }
    }
}
