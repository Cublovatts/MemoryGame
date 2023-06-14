using System.Collections;
using UnityEngine;

public class CardComparer : MonoBehaviour
{
    public static CardComparer Instance;
    public bool IsComparisonInProgress = false;

    [SerializeField]
    private AudioClip _matchSound;

    private AudioSource _audioSource;
    private Card _previouslySelectedCard;
    private Card _currentlySelectedCard;
    
    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
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
            StartCoroutine(playSoundWithDelay(_matchSound, 0.5f));
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

    IEnumerator playSoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        _audioSource.PlayOneShot(clip);
    }
}
