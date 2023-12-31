using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupManager : MonoBehaviour
{
    private readonly string POSSIBLE_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [SerializeField]
    private GameObject _cardPrefab;
    [SerializeField]
    private int _numberOfCards;
    [SerializeField]
    private LevelTimer _levelTimer;

    private List<Card> _cards = new List<Card>();
    private float _currentOffset = 0f;
    private float _offset = 3f;

    void Start()
    {
        // Check for even number of cards
        if (_numberOfCards % 2 != 0)
        {
            _numberOfCards++;
        }

        CardComparer.Instance.MaxMatches = _numberOfCards/2;

        SetCards();
    }

    [ContextMenu("Set Cards")]
    public void SetCards()
    {
        _currentOffset = 0f;
        if (_cards.Count == 0)
        {
            for (int i = 0; i < _numberOfCards; i++)
            {
                GameObject newCard = Instantiate(_cardPrefab, gameObject.transform);
                newCard.transform.Translate(new Vector3(_currentOffset, 0, 0));
                _currentOffset += _offset;

                _cards.Add(newCard.GetComponentInChildren<Card>());
            }
        } else
        {
            foreach (Card card in _cards)
            {
                card.SetFaceValue("");
            }
        }

        foreach (Card card in _cards)
        {
            card.HideCard();
        }

        for (int i=0; i<_numberOfCards/2; i++)
        {
            int initialisedCards = 0;
            char cardValue = POSSIBLE_CHARS[Random.Range(0, 25)];
            while (initialisedCards < 2)
            {
                int randomCard = Random.Range(0, _numberOfCards);
                Card currentCard = _cards[randomCard];
                if (currentCard.FaceValue == "" && initialisedCards < 2)
                {
                    currentCard.FaceValue = cardValue.ToString();
                    StartCoroutine(SetFaceValueWithDelay(currentCard, cardValue.ToString(), 1f));
                    initialisedCards++;
                }
            }
        }

        CardComparer.Instance.ResetMatches();
        _levelTimer.StopTimer();
        _levelTimer.ResetTimer();
    }

    IEnumerator SetFaceValueWithDelay(Card card, string value, float delay)
    {
        yield return new WaitForSeconds(delay);
        card.SetFaceValue(value);
    }
}
