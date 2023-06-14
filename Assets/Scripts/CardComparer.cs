using UnityEngine;

public class CardComparer : MonoBehaviour
{
    public static CardComparer Instance;
    public bool IsComparisonInProgress = false;

    private Card _previouslySelectedCard;
    private Card _currentlySelectedCard;
    
    private void Awake()
    {
        Instance = this;
    }

    public void SubmitCard(Card card)
    {
        if (_previouslySelectedCard == null)
        {
            _previouslySelectedCard = card;
            return;
        }

        IsComparisonInProgress = true;

        if (_previouslySelectedCard.GetCardValue() == card.GetCardValue())
        {
            // It's a match leave the cards face up
            _previouslySelectedCard = null;
            _currentlySelectedCard = null;
            IsComparisonInProgress = false;
        } else
        {
            // The cards don't match, flip them both back
            _currentlySelectedCard = card;
            Invoke("FlipCardsBack", 1f);
        }
    }

    private void FlipCardsBack()
    {
        _previouslySelectedCard.HideCard();
        _currentlySelectedCard.HideCard();
        _previouslySelectedCard = null;
        _currentlySelectedCard = null;
        IsComparisonInProgress = false;
    } 
}
