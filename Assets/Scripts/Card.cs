using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string FaceValue;

    [SerializeField]
    private AudioClip _cardFlipSound;

    private AudioSource _audioSource;
    private Animator _animator;
    private CardComparer _cardComparer;
    private TMP_Text _faceText;
    private bool _isSelectable = true;

    void Awake()
    {
        _faceText = GetComponentInChildren<TMP_Text>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _faceText.text = FaceValue;
        _cardComparer = CardComparer.Instance;
    }


    public void OnMouseDown()
    {
        if (_isSelectable && !_cardComparer.IsComparisonInProgress)
        {
            _cardComparer.SubmitCard(this);

            ShowCard();
        }
    }

    public string GetCardValue()
    {
        return FaceValue;
    }

    public void SetFaceValue(string value)
    {
        FaceValue = value;
        _faceText.text = FaceValue;
    }

    [ContextMenu("Show Card")]
    public void ShowCard()
    {
        //gameObject.transform.SetPositionAndRotation(transform.position, new Quaternion(0f, 180f, 0f, 0f));
        _isSelectable = false;
        _audioSource.PlayOneShot(_cardFlipSound);
        _animator.SetBool("IsShowing", true);
    }

    public void HideCard()
    {
        //gameObject.transform.SetPositionAndRotation(transform.position, new Quaternion(0f, 0f, 0f, 0f));
        _isSelectable = true;
        _animator.SetBool("IsShowing", false);
    }

    public void FlipCard()
    {
        gameObject.transform.Rotate(0, 180, 0);
    }
}
