using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image _buttonImage;

    void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PressingAction();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonImage.color = Color.white;
    }

    protected virtual void PressingAction()
    {
        
    }
}
