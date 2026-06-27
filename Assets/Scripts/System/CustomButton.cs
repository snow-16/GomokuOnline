using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 各種ボタン用基底クラス
/// </summary>
public abstract class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary> ボタンを押す権限があるか </summary>
    [SerializeField]
    protected bool _interactable = true;

    /// <summary> ボタンの画像 </summary>
    private Image _buttonImage;

    void Awake()
    {
        _buttonImage = GetComponent<Image>();

        if(!_interactable)
        {
            _buttonImage.color = Color.gray;
        }

        OnAwaking();
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

    /// <summary>
    /// ボタンの初期設定
    /// </summary>
    protected virtual void OnAwaking()
    {
        
    }

    /// <summary>
    /// ボタン押下時のアクション
    /// </summary>
    protected virtual void PressingAction()
    {
        
    }

    /// <summary>
    /// ボタンを押す権限の切り替え
    /// </summary>
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
