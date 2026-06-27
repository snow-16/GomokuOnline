using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

/// <summary>
/// ロビーからのセッション新設ボタンクラス
/// </summary>
public class NewwRoomButton : CustomButton
{
    /// <summary> 部屋コード入力フィールド </summary>
    [SerializeField]
    private TMP_InputField _codeInputField;
    /// <summary> 部屋コードエラー表示テキストUI </summary>
    [SerializeField]
    private TextMeshProUGUI _massageText;

    protected override void PressingAction()
    {
        if(_codeInputField.text.Length == 0)
        {
            RelayManager.CreateRoom();
            transform.parent.gameObject.SetActive(false);
            transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        }
        else if(_codeInputField.text.Length == 6)
        {
            if(!Regex.IsMatch(_codeInputField.text, @"^[A-Z]+$"))
            {
                _massageText.gameObject.SetActive(true);
                _massageText.text = "コードはアルファベット大文字のみです";
                _codeInputField.text = "";
            }
            else if(!LobbyData._sessionPassList.Contains(_codeInputField.text))
            {
                RelayManager.CreateRoom(_codeInputField.text);
                transform.parent.gameObject.SetActive(false);
                transform.parent.parent.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                _massageText.gameObject.SetActive(true);
                _massageText.text = "そのコードは既に存在します";
                _codeInputField.text = "";
            }
        }
        else
        {
            _massageText.gameObject.SetActive(true);
            _massageText.text = "コードが6文字ではありません";
            _codeInputField.text = "";
        }
    }
}
