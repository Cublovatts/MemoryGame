using System.Collections;
using UnityEngine;

public class CardComparer : MonoBehaviour
{
    public static CardComparer Instance;
    public bool IsComparisonInProgress = false;
    public int MaxMatches;

    [SerializeField]
    private AudioClip _matchSound;
    [SerializeField]
    private LevelTimer _levelTimer;

    private AudioSource _audioSource;
    private Card _previouslySelectedCard;
    private Card _currentlySelectedCard;
    private int _currentMatches = 0;
    
    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void SubmitCard(Card card)
    {
        _levelTimer.StartTimer();
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
            _currentMatches++;
            if (_currentMatches == MaxMatches)
            {
                _levelTimer.StopTimer();
            }
        } else
        {
            // The cards don't match, flip them both back
            _currentlySelectedCard = card;
            Invoke("FlipCardsBack", 1f);
        }
    }

    public void ResetMatches()
    {
        _currentMatches = 0;
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
