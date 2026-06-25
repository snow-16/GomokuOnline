using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    protected bool _interactable = true;

    private Image _buttonImage;

    void Awake()
    {
        _buttonImage = GetComponent<Image>();

        if(!_interactable)
        {
            _buttonImage.color = Color.gray;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_interactable)
        {
            PressingAction();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_interactable)
        {
            _buttonImage.color = Color.gray;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_interactable)
        {
            _buttonImage.color = Color.white;
        }
    }

    protected virtual void PressingAction()
    {
        
    }

    public void ChangeInteractable()
    {
        _interactable = !_interactable;

        if(!_interactable)
        {
            _buttonImage.color = Color.gray;
        }
        else
        {
            _buttonImage.color = Color.white;
        }
    }
}
