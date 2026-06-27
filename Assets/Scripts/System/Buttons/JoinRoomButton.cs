using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// ロビーからセッションへの入室ボタンクラス
/// </summary>
public class JoinRoomButton : CustomButton
{
    /// <summary> 部屋コード入力フィールド </summary>
    [SerializeField]
    private TMP_InputField _codeInputField;
    /// <summary> 部屋コードエラー表示テキストUI </summary>
    [SerializeField]
    private TextMeshProUGUI _massageText;

    protected override void PressingAction()
    {
        if(LobbyData._sessionPassList.Contains(_codeInputField.text))
        {
            RelayManager.JoinRoom(_codeInputField.text);
            transform.parent.gameObject.SetActive(false);
            transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        }
        else if(_codeInputField.text.Length == 0)
        {
            _massageText.gameObject.SetActive(true);
            _massageText.text = "コードを入力してください";
            _codeInputField.text = "";
        }
        else if(_codeInputField.text.Length != 6)
        {
            _massageText.gameObject.SetActive(true);
            _massageText.text = "コードが6文字ではありません";
            _codeInputField.text = "";
        }
        else
        {
            if(!Regex.IsMatch(_codeInputField.text, @"^[A-Z]+$"))
            {
                _massageText.gameObject.SetActive(true);
                _massageText.text = "コードはアルファベット大文字のみです";
                _codeInputField.text = "";
            }
            else
            {
                _massageText.gameObject.SetActive(true);
                _massageText.text = "コードが間違っています";
                _codeInputField.text = "";
            }
        }
    }
}
