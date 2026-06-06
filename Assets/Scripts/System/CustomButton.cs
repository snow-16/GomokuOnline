using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnityEvent _plessingAction;
    private Image _buttonImage;

    void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _plessingAction?.Invoke();
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonImage.color = Color.white;
    }
}
