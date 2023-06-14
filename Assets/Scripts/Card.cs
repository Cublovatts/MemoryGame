using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardComparer _cardComparer;
    private TMP_Text _faceText;
    [SerializeField]
    private string _faceValue;
    private bool _isSelectable = true;

    void Awake()
    {
        _faceText= GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        _faceText.text = _faceValue;
        _cardComparer = CardComparer.Instance;
    }


    public void OnMouseDown()
    {
        if (_isSelectable)
        {
            _cardComparer.SubmitCard(this);

            ShowCard();
        }
    }

    public string GetCardValue()
    {
        return _faceValue;
    }

    public void ShowCard()
    {
        gameObject.transform.SetPositionAndRotation(transform.position, new Quaternion(0f, 180f, 0f, 0f));
        _isSelectable = false;
    }

    public void HideCard()
    {
        gameObject.transform.SetPositionAndRotation(transform.position, new Quaternion(0f, 0f, 0f, 0f));
        _isSelectable = true;
    }

    public void FlipCard()
    {
        gameObject.transform.Rotate(0, 180, 0);
    }
}
